using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private static GameManager _Inst;
    public BlockDataBase blockDataBase;
    public UIManager uiManager;
    public ATBTimer atbTimer;
    public List<Character> characters;

    public static GameManager Inst {
        get {
            if (_Inst == null) { Debug.Log("Gamanager Singleton error"); }
            return _Inst;
        }
    }
	// Use this for initialization
	void Start () {
        _Inst = this;
        blockDataBase = this.GetComponent<BlockDataBase>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        atbTimer = this.GetComponent<ATBTimer>();
        characters = new List<Character>();

        for (int i = 0; i < this.transform.childCount; i++) {
            Character temp =this.transform.GetChild(i).GetComponent<Character>(); 
            characters.Add(temp);    
        }


        blockDataBase.Initial();
        uiManager.Initial();
        atbTimer.Initial(10.0f);
	}
	
	// Update is called once per frame
	void Update () {

        blockDataBase.MyUpdate();
        atbTimer.MyUpdate();
        uiManager.MyUpdate();
	}
}
