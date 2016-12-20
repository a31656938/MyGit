package application.model;

public class Score extends Model{
	int score;
	@Override
	public void SetData(Object data) {
		score = (int)data;		
		Notify();
	}

	@Override
	public Object GetData() {
		return (Object)score;
	}

}
