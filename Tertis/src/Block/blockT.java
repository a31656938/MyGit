package Block;

import application.model.Board;
import application.pair;

public class blockT extends Block{

	public blockT(){
		this.matrix = new pair[4];
		int x = Board.WIDTH/2;
		matrix[0] = new pair(x-1,0);
		matrix[1] = new pair(x  ,0);
		matrix[2] = new pair(x+1,0);
		matrix[3] = new pair(x,1);
		color = 2;
	}
	@Override
	public pair[] rotate(int side) {
		return this.matrix;
	}
	
}
