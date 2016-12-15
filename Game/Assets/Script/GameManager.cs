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

        AttackBlock temp0 = new AttackBlock("oil", "000", 3, 5); 
        IFBlock temp = new IFBlock("00", "0000", 2, temp0);
        MultipleBlock temp1 = new MultipleBlock("11", "1111", 1, temp); 
        uiManager.showBlockUI(temp1, uiManager.program, Vector3.zero); 


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
            // IF指到方塊群組方塊
            Vector2 nowGrid = new Vector2(-1,-1);
            List<BlockObj> blockObjs = new List<BlockObj>();
            foreach (RaycastResult hit in uiRayCast.Raycast()) {
                if (hit.gameObject.name == "program") nowGrid = CalculateGrid();
                BlockObj blockObj = hit.gameObject.GetComponent<BlockObj>();
                if (blockObj != null) blockObjs.Add(blockObj); 
            }
            if (blockObjs.Count >= 1 && nowDragBlock == null) uiManager.ChangeDescription(blockObjs[0].block);
            else uiManager.HideDescription();
            
            //拖曳方塊判斷
            if(blockObjs.Count==1){
                if (Input.GetMouseButtonDown(0)) {
                    if (nowGrid != new Vector2(-1, -1)) removeBlock(nowGrid);
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
                    SetBlock(nowGrid, nowDragBlock);
                    uiManager.dragBlock.SetActive(false);
                    nowDragBlock = null;
                }
            }
            
        }
    }
    void removeBlock(Vector2 position) {

        for (int i = 0; i < characterManager.TotalMemory; i++)
        {
            Block exist = characterManager.characters[(int)position.x].process[i];
            if (exist.name != null){
                int existStart = i;
                int existEnd = i + exist.GetCast() - 1;
                if (existStart <= position.y && position.y <= existEnd){
                    characterManager.characters[(int)position.x].process[i] = new Block();
                    break;
                }
            }
        }

        uiManager.UpdateMemory();
    }
    void SetBlock(Vector2 nowGrid ,Block block) {
        if (nowGrid == new Vector2(-1, -1)) return;
        int nowStart = (int)nowGrid.y;
        int nowEnd = (int)nowGrid.y+block.GetCast() - 1;
        // 超過最底
        if (nowEnd >= characterManager.TotalMemory) return;
        // 0 不存
        // 1 存
        // 2 in
        int flag = 1;
        for (int i = 0; i < characterManager.TotalMemory; i++) {
            Block exist =characterManager.characters[(int)nowGrid.x].process[i];
            if (exist.name != null) { 
                int existStart = i;
                int existEnd = i+exist.GetCast() - 1;
                if (existStart <= nowEnd && existEnd >= nowStart){
                    if (typeof(BlockDecorator).IsAssignableFrom(exist.GetType())) {
                        ((BlockDecorator)exist).SetIn(block);
                        flag = 2;
                    }
                    else flag = 0;
                    break;
                }
            }
        }
        // 更改 memory  更新UI
        if (flag !=0) {
            if (flag == 1)characterManager.characters[(int)nowGrid.x].process[(int)nowGrid.y] = block;
            
            uiManager.UpdateMemory();
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

        if (index == 0){
            blockDataBase.attackParent.gameObject.SetActive(true);
            uiManager.nowGroupName.text = "Attack";
        }
        else if (index == 1){
            blockDataBase.buffParent.gameObject.SetActive(true);
            uiManager.nowGroupName.text = "Buff";
        }
        else if (index == 2) {
            blockDataBase.IFParent.gameObject.SetActive(true);
            uiManager.nowGroupName.text = "IF";
        }
        else if (index == 3){
            blockDataBase.multipleParent.gameObject.SetActive(true);
            uiManager.nowGroupName.text = "Multiple";
        }
        blockDataBase.nowBlocks.GetComponent<RectTransform>().sizeDelta = new Vector2(260,blockDataBase.panelHeight[index]);
    }
    public void StartButton() {
        start = !start;
        uiManager.blocksAnimator.SetTrigger("hide");
        if (start){
            characterManager.calculateTime();
            atbTimer.ReStart();
            uiManager.startButtonText.text = "撤退";
        }
        else uiManager.startButtonText.text = "出擊";
    }

}
