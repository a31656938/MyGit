using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager _Inst;
    public BlockDataBase blockDataBase;
    public UIManager uiManager;
    public CharacterManager characterManager;
    public ATBTimer atbTimer;


    public static GameManager Inst {
        get {
            if (_Inst == null) { Debug.Log("Gamanager Singleton error"); }
            return _Inst;
        }
    }
	// Use this for initialization
	void Start () {
        _Inst = this;
        blockDataBase = GameObject.Find("BlockDataBase").GetComponent<BlockDataBase>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        atbTimer = this.GetComponent<ATBTimer>();


        characterManager.Initial();
        blockDataBase.Initial();
        uiManager.Initial();
        atbTimer.Initial(10.0f);

        ChangeNowBlockGroup("Attack");
	}
	
	// Update is called once per frame
	void Update () {

        characterManager.MyUpdate();
        atbTimer.MyUpdate();
        uiManager.MyUpdate();
	}
    public void ChangeNowBlockGroup(string group) {
        blockDataBase.setAllActive(false);

        if (group == "Attack") blockDataBase.attackParent.gameObject.SetActive(true);
        else if (group == "Buff") blockDataBase.buffParent.gameObject.SetActive(true);

        uiManager.nowGroupName.text = group;
    }
}
