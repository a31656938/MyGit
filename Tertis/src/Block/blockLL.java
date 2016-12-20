package Block;

import application.model.Board;
import application.pair;

public class blockLL extends Block{

	public blockLL(){
		this.matrix = new pair[4];
		int x = Board.WIDTH/2;
		matrix[0] = new pair(x+1,1);
		matrix[1] = new pair(x+1,0);
		matrix[2] = new pair(x  ,1);
		matrix[3] = new pair(x-1,1);
		nowState = 0;
		color = 5;
	}
	@Override
	public void rotate(int side,int[][] board) {
		pair[] temp = new pair[4];
		
		temp[0] = new pair(matrix[0].x,matrix[0].y);
		nowState +=side;
		if(nowState<0)nowState = 999;
		if(nowState%4 == 0){
			temp[1]= new pair(temp[0].x     , temp[0].y - 1);
			temp[2]= new pair(temp[0].x - 1 , temp[0].y);
			temp[3]= new pair(temp[0].x - 2 , temp[0].y);
		}
		else if(nowState%4 == 1){
			temp[1]= new pair(temp[0].x + 1 , temp[0].y);
			temp[2]= new pair(temp[0].x     , temp[0].y - 1);
			temp[3]= new pair(temp[0].x     , temp[0].y - 2);
		}
		else if(nowState%4 == 2){
			temp[1]= new pair(temp[0].x     , temp[0].y + 1);
			temp[2]= new pair(temp[0].x + 1 , temp[0].y);
			temp[3]= new pair(temp[0].x + 2 , temp[0].y);
		}
		else if(nowState%4 == 3){
			temp[1]= new pair(temp[0].x - 1 , temp[0].y);
			temp[2]= new pair(temp[0].x     , temp[0].y + 1);
			temp[3]= new pair(temp[0].x     , temp[0].y + 2);
		}
		
		if(!checkCollision(board,temp)){
			nowState -= side;
		}
	}
	
}
