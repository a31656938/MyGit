using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public BlockDataBase blockDataBase;
    public UIManager uiManager;
    public ATBTimer atbTimer;
    public List<Character> characters;
	// Use this for initialization
	void Start () {
        blockDataBase = this.GetComponent<BlockDataBase>();
        uiManager = this.GetComponent<UIManager>();
        atbTimer = this.GetComponent<ATBTimer>();
        characters = new List<Character>();

        List<ATBCharacter> tempATBS = new List<ATBCharacter>();
        for (int i = 0; i < this.transform.childCount; i++) {
            Character temp =this.transform.GetChild(i).GetComponent<Character>(); 
            characters.Add(temp);    
            tempATBS.Add(temp.atb);
        }


        blockDataBase.Initial();
        uiManager.Initial();
        atbTimer.Initial(10.0f,5.0f,ref tempATBS);
	}
	
	// Update is called once per frame
	void Update () {

        blockDataBase.MyUpdate();
        atbTimer.MyUpdate();
        uiManager.MyUpdate();
	}
}
