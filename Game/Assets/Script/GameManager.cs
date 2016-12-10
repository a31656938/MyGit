using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private static GameManager _Inst;
    public BlockDataBase blockDataBase;
    public UIManager uiManager;
    public CharacterManager characterManager;
    public ATBTimer atbTimer;
    public UIRayCast uiRayCast;
    public bool start;
    public Vector2 nowGrid;
    public Block nowDragBlock;
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
            List<BlockObj> blockObjs = new List<BlockObj>();
            foreach (RaycastResult hit in uiRayCast.Raycast()) {
                if (hit.gameObject.name == "program") nowGrid = CalculateGrid();
                BlockObj blockObj = hit.gameObject.GetComponent<BlockObj>();
                if (blockObj != null) blockObjs.Add(blockObj); 
            }
            if (blockObjs.Count == 1 && nowDragBlock == null) uiManager.ChangeDescription(blockObjs[0].block);
            else uiManager.HideDescription();
            
            if(blockObjs.Count==1){
                if (Input.GetMouseButtonDown(0)) {
                    nowDragBlock = blockObjs[0].block;
                    uiManager.dragBlock.SetActive(true);
                }
            }
            if (nowDragBlock != null)
            {
                if (Input.GetMouseButton(0))
                {
                    uiManager.UpdateDragBlock(nowDragBlock);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    nowDragBlock = null;
                    uiManager.dragBlock.SetActive(false);
                }
            }
            
        }
    }

    Vector2 CalculateGrid() {
        Vector2 programC = new Vector2(Input.mousePosition.x - 1280, Input.mousePosition.y);
        int x = (int)(programC.x / (640.0f / 3.0f)); 
        int y = (int)(programC.y / (1020.0f / (float)characterManager.TotalMemory));
        Vector3 re = new Vector2(x, characterManager.TotalMemory - y -1); 
        return re;
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
