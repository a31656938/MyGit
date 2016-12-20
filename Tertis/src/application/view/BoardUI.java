package application.view;

import application.Input;
import application.model.Board;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;

public class BoardUI extends MyObserver{
	
	private static final int BLOCK_SIZE = 29;
	
	int[][] data;
	
	@FXML
	private GridPane gamePanel;
	
	private Rectangle[][] displayMatrix;
	
	@FXML
	private void initialize(){
		displayMatrix = new Rectangle[Board.HEIGHT][Board.WIDTH];
		for(int i = 0; i < Board.HEIGHT - 1; i++){
			for(int j = 0 ; j < Board.WIDTH ; j++){
				displayMatrix[i][j] = new Rectangle(BLOCK_SIZE, BLOCK_SIZE);
				displayMatrix[i][j].setFill(Color.TRANSPARENT);
				gamePanel.add(displayMatrix[i][j], j, i);
			}
		}
		
		gamePanel.addEventHandler(KeyEvent.KEY_PRESSED,  (key) -> {
			Input.setKey(key.getCode(), true);
		});
		
		gamePanel.addEventHandler(KeyEvent.KEY_RELEASED, (key) -> {
			Input.setKey(key.getCode(), false);
		});
	}
	
	
	@Override
	public void Update() {
		data = (int[][])model.GetData();
		showBoard();
	}
	
	public void showBoard(){
		// update board UI
		for(int j = 1; j < Board.HEIGHT; j++){
			for(int i = 0; i < Board.WIDTH; i++){
				displayMatrix[j - 1][i].setFill(toFXColor(data[i][j]));
			}
		}
		
	}
	
	private Color toFXColor(int color){
		switch(color){
		case 0: return Color.WHITE;
		case 1: return Color.BLUE;
		case 2: return Color.RED;
		case 3: return Color.YELLOW;
		case 4: return Color.LIME;
		case 5: return Color.ORANGERED;
		case 6: return Color.CYAN;
		case 7: return Color.FUCHSIA;
		}
		return Color.BLACK;
	}
	
}
