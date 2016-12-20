package application.model;
import Block.Block;

public class Board extends Model{
	
	public static int WIDTH = 9;
	public static int HEIGHT = 21;
	
	public Block nowBlock;
	public int[][] boards;
	
	@Override
	public void SetData(Object data) {
		boards = (int[][])data;		
		Notify();
	}

	@Override
	public Object GetData() {
		int[][] temp = new int[Board.WIDTH][Board.HEIGHT];
		
		for(int j = 0; j < Board.HEIGHT; j++){
			for(int i = 0; i < Board.WIDTH; i++){
				temp[i][j] = boards[i][j];
			}
		}	
		
		for(int i=0;i<nowBlock.GetMatrix().length;i++){
			temp[nowBlock.GetMatrix()[i].x][nowBlock.GetMatrix()[i].y] = nowBlock.color;
		}
		
		
		return (Object)temp;
	}

}
