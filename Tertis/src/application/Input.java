package application;

import javafx.scene.input.KeyCode;

public class Input {
	private static boolean[] keyStates = new boolean[1000] ;
	
	public static boolean key(KeyCode keyCode){
		int ikey = toIKey(keyCode);
		return keyStates[ikey];
	}
	
	public static  void setKey(KeyCode keyCode, boolean state){
		int ikey = toIKey(keyCode);
		keyStates[ikey] = state;
	}
	
	public  static int toIKey(KeyCode keyCode){
		switch(keyCode){
		case W: return 0;
		case A: return 1;
		case S: return 2;
		case D: return 3;
		case Z: return 4;
		case X: return 5;
		case C: return 6;
		case SPACE: return 7;
		case UP: return 100;
		case DOWN: return 101;
		case RIGHT: return 102;
		case LEFT: return 103;
		default:
			return 999;
		}
	}
}
