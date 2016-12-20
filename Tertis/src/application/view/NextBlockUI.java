package application.view;

import Block.Block;

public class NextBlockUI extends MyObserver{
	Block data;
	@Override
	public void Update() {
		data = (Block)model.GetData();
		showNextBlock();
	}
	public void showNextBlock(){
		// update next block UI
	}
}
