using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    public ATBUI atbUI;
    

	// Use this for initialization
	public void Initial () {
        atbUI = this.GetComponent<ATBUI>();


        atbUI.Initial();
	}
	
	// Update is called once per frame
	public void MyUpdate () {
        atbUI.MyUpdate();
	}
}
