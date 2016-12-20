package application.view;

public class ScoreUI extends MyObserver{
	int data;
	@Override
	public void Update() {
		data = (int)model.GetData();
		showScore();
	}
	public void showScore(){
		// update score UI
	}
}
