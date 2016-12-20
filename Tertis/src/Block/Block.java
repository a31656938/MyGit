package Block;

import application.pair;
import application.model.Board;

public abstract class Block implements blockInterface{
	protected pair[] matrix;
	public int color;
	
	public pair[] GetMatrix(){
		return matrix;
	}
	public boolean checkCollision(int[][] board, pair[] set){
		boolean check = true;
		
		
		
		for(int i=0;i<set.length;i++){
			if(set[i].x < 0 || set[i].x >= Board.WIDTH)check = false;
			else if(set[i].y < 0 || set[i].y >= Board.HEIGHT)check = false;
			else if(board[set[i].x][set[i].y] != 0) check = false;
		}
		
		if(check) matrix = set;
		
		return check;
	}
	public void moveLeft(int[][] board){
		pair[] temp = new pair[matrix.length];
		for(int i=0;i<temp.length;i++){
			temp[i] = new pair(matrix[i].x - 1 , matrix[i].y);
		}
		checkCollision(board,temp);
	}
	public void moveRight(int[][] board){
		pair[] temp = new pair[matrix.length];
		for(int i=0;i<temp.length;i++){
			temp[i] = new pair(matrix[i].x + 1 , matrix[i].y);
		}
		checkCollision(board,temp);
	}
	public boolean moveDown(int[][] board){
		pair[] temp = new pair[matrix.length];
		for(int i=0;i<temp.length;i++){
			temp[i] = new pair(matrix[i].x , matrix[i].y + 1);
		}
		return checkCollision(board,temp);
		
	}
}
