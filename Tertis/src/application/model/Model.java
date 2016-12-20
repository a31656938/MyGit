package application.model;
import java.util.*;

import application.view.MyObserver;

public abstract class Model {
	ArrayList<MyObserver> observers;
	
	public void Attach(MyObserver observer){
		if(observers == null) observers = new ArrayList<MyObserver>();
		observers.add(observer);		
	}
	public void Detach(MyObserver observer){
		observers.remove(observer);
	}
	public void Notify(){
		for(MyObserver o : observers){
			o.Update();
		}
	}
	public abstract void SetData(Object data);
	public abstract Object GetData();
}
