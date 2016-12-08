using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GameManager : MonoBehaviour {
    private static GameManager _Inst;
    public BlockDataBase blockDataBase;
    public UIManager uiManager;
    public CharacterManager characterManager;
    public ATBTimer atbTimer;
    public UIRayCast uiRayCast;
    public bool start;
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
        uiRayCast = GameObject.Find("Canvas").GetComponent<UIRayCast>();
        atbTimer = this.GetComponent<ATBTimer>();

        start = false;

        characterManager.Initial();
        blockDataBase.Initial();
        uiManager.Initial();
        atbTimer.Initial(10.0f);

        ChangeNowBlockGroup(0);
	}
	
	// Update is called once per frame
	void Update () {

        if (start)//戰鬥
        {
            characterManager.MyUpdate();
            atbTimer.MyUpdate();
            uiManager.MyUpdate();
        }
        else // 編輯 
        {
            foreach (RaycastResult hit in uiRayCast.Raycast()) {
                BlockObj blockObj = hit.gameObject.GetComponent<BlockObj>();
                if (blockObj != null) {
                    Debug.Log(blockObj.block.GetDescription());
                
                
                }
            
            }
        
        
        
        }
    }


    public void ChangeNowBlockGroup(int index) {
        blockDataBase.setAllActive(false);

        if (index == 0)
        {
            blockDataBase.attackParent.gameObject.SetActive(true);
            uiManager.nowGroupName.text = "Attack";
        }
        else if (index == 1)
        {
            blockDataBase.buffParent.gameObject.SetActive(true);
            uiManager.nowGroupName.text = "Buff";
        }
        blockDataBase.nowBlocks.GetComponent<RectTransform>().sizeDelta = new Vector2(260,blockDataBase.panelHeight[index]);
    }
    public void StartButton() {
        start = !start;
        uiManager.blocksAnimator.SetTrigger("hide");
        if (start){
            atbTimer.ReStart();
            uiManager.startButtonText.text = "撤退";
        }
        else uiManager.startButtonText.text = "出擊";
    }

}
