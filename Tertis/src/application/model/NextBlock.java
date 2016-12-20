package application.model;
import Block.Block;

public class NextBlock extends Model{
	Block nextBlock;
	@Override
	public void SetData(Object data) {
		nextBlock = (Block)data;
		Notify();
	}

	@Override
	public Object GetData(){
		return (Block)nextBlock;
	}

}
