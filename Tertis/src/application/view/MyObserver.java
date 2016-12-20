package application.view;

import application.model.Model;

public abstract class MyObserver {
	protected Model model;
	
	public abstract void Update();
	public void SetModel(Model model){
		this.model = model;
	}

}
