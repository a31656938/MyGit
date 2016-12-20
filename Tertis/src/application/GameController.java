package application;


import java.io.IOException;
import java.util.Random;

import Block.*;
import application.model.*;
import application.view.*;

import javafx.animation.*;
import javafx.fxml.*;
import javafx.scene.input.KeyCode;
import javafx.scene.layout.*;
import javafx.util.Duration;


public class GameController  {
	public boolean stop;
	public boolean gameOver;
	public float boardTimer;
	public float keyTimer;
	
	private BoardUI boardUI;
	private NextBlockUI nextBlockUI;
	private ScoreUI scoreUI;
	private Board board;
	private NextBlock nextBlock;
	private Score score;
	private Timeline timeLine;
	private boolean quickDown;
	
	@FXML
	private BorderPane pane;
	
	@FXML
    private void initialize() {
	       
	        initBoardUI();
	        initNextBlockUI();
	        initScoreUI();
	        
	        resetGame();
	        
	        timeLine = new Timeline(new KeyFrame(Duration.millis(10), ae -> mainLoop()));
	        timeLine.setCycleCount(Timeline.INDEFINITE);
	        timeLine.play();
    }
	private void initBoardUI(){
		 try{
	        	FXMLLoader loader = new FXMLLoader();
	            loader.setLocation(GameController.class.getResource("view/BoardUI.fxml"));
	            AnchorPane boardPane = loader.load();
	            pane.setLeft(boardPane);
	            boardUI = loader.getController();
	            board = new Board();
	            boardUI.SetModel(board);
	            board.Attach(boardUI);
	        }catch(IOException e){
	        	e.printStackTrace();
	        }
	}
	private void initNextBlockUI(){
		 try{
	        	FXMLLoader loader = new FXMLLoader();
	            
	            loader.setLocation(GameController.class.getResource("view/NextBlockUI.fxml"));
	            AnchorPane nextBlockPane = loader.load();
	            nextBlockUI = loader.getController();
	            nextBlock = new NextBlock();
	            nextBlockUI .SetModel(nextBlock);
	            nextBlock.Attach(nextBlockUI);
	        }catch(IOException e){
	        	e.printStackTrace();
	        }
	}
	private void initScoreUI(){
		 try{
	        	FXMLLoader loader = new FXMLLoader();
	            loader.setLocation(GameController.class.getResource("view/ScoreUI.fxml"));
	            AnchorPane scorePane = loader.load();
	            scoreUI= loader.getController();
	            score = new Score();
	            scoreUI.SetModel(score);
	            score.Attach(scoreUI);
	        }catch(IOException e){
	        	e.printStackTrace();
	        }
	}
	public void resetGame(){
		boardTimer = 0;
		keyTimer = 0;
		stop = false;
		quickDown = false;
		gameOver = false;
		int[][] temp ;
		temp = new int[Board.WIDTH][Board.HEIGHT];
		
		for(int j = 0; j < Board.HEIGHT; j++){
			for(int i = 0; i < Board.WIDTH; i++){
				temp[i][j] = 0;
			}
		}	
		board.nowBlock = randomNext();
		board.SetData(temp);	// Initial board
		nextBlock.SetData(randomNext());	// next
		score.SetData(0);		// zero score	
	}

	private void mainLoop(){
		// IF stop 
		if(stop || gameOver) return;
		// 
		updateBoard();
		
		gameOver = checkGameOver();
	}
	Block randomNext(){
		Random ran = new Random();
		int i = ran.nextInt(7);
		switch(i){
			case 0: return new blockO();
			case 1: return new blockI();
			case 2: return new blockL();
			case 3: return new blockLL();
			case 4: return new blockZ();
			case 5: return new blockZZ();
			case 6: return new blockT();
		}
		return null;
	}
	void checkDelete(){
		for(int i = Board.HEIGHT - 1; i >= 0; i--){
			boolean check = true;
			for(int j=0;j<Board.WIDTH;j++){
				if(board.boards[j][i] == 0) check = false;
			}
			if(check){
				score.SetData((int)score.GetData() + 100);
				// down one step
				for(int k= i;k>=1;k--){
					for(int j=0;j<Board.WIDTH;j++){
						board.boards[j][k] = board.boards[j][k - 1];
					}		
				}	
				i++;
			}
		}
			
	}
	void updateBoard(){
		if(board.nowBlock == null){
			board.nowBlock = (Block)nextBlock.GetData();
			nextBlock.SetData(randomNext());
		}
		
		if(keyTimer >= 0.1f){
			// input
			if(Input.key(KeyCode.LEFT)) board.nowBlock.moveLeft(board.boards);
			else if(Input.key(KeyCode.RIGHT)) board.nowBlock.moveRight(board.boards);
			else if(Input.key(KeyCode.DOWN)) board.nowBlock.moveDown(board.boards);
			else if(Input.oneKey(KeyCode.Z)) board.nowBlock.rotate(-1,board.boards);
			else if(Input.oneKey(KeyCode.X)) board.nowBlock.rotate(1,board.boards);
			else if(Input.oneKey(KeyCode.SPACE)){
				quickDown = true;
			}
			keyTimer = 0;
		}
		
		if(quickDown && boardTimer >= 0.02f){
			if( !board.nowBlock.moveDown(board.boards)){
				quickDown = false;
				board.SetData(board.GetData());
				checkDelete();
				board.SetData(board.boards);
				board.nowBlock = null;
			}
			boardTimer = 0;
		}else if (boardTimer >= 1){
			
			if( !board.nowBlock.moveDown(board.boards)){
				quickDown = false;
				board.SetData(board.GetData());
				checkDelete();
				board.SetData(board.boards);
				board.nowBlock = null;
			}
			
			boardTimer = 0;
		}else board.SetData(board.boards);
		
		keyTimer += 0.01;
		boardTimer += 0.01;
	}
	boolean checkGameOver(){
		int[][] temp = board.boards;
		for(int i=0;i<Board.WIDTH;i++){
			if(temp[i][1] != 0){
				return true;
			}
		}
		return false;
	}
}













