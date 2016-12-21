using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {
    private static GameManager _Inst;
    public BlockDataBase blockDataBase;
    public UIManager uiManager;
    public CharacterManager characterManager;
    public ATBTimer atbTimer;
    public UIRayCast uiRayCast;
    
    public bool start;
    public float startDelay;
    
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
        startDelay = 0;

        characterManager.Initial();
        atbTimer.Initial(15.0f);
        uiManager.Initial();
        uiManager.BattleUI(0);
        uiManager.MyUpdate();
        blockDataBase.Initial();

        ChangeNowBlockGroup(0);
	}
	
	// Update is called once per frame
	void Update () {

        if (start)//戰鬥
        {
            if (startDelay <= 3){
                if (startDelay == 0) uiManager.blocksAnimator.SetTrigger("hide");

                uiManager.BattleUI((startDelay - 1.0f) / 1.0f);

                startDelay += Time.deltaTime;
            }
            else {
                foreach (Character c in characterManager.characters) {
                    c.atb.nowSpeed = characterManager.normalSpeed;
                }
                // 加速 緩速
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Input.GetMouseButton(0)) {
                    if (Physics.Raycast(ray, out hit)){
                        int index = 0;
                        if (Int32.TryParse(hit.transform.name, out index)) {
                            characterManager.characters[index].atb.nowSpeed += 0.5f;
                        }
                    } 
                }
                if (Input.GetMouseButton(1)){
                    if (Physics.Raycast(ray, out hit)){
                        int index = 0;
                        if (Int32.TryParse(hit.transform.name, out index)){
                            characterManager.characters[index].atb.nowSpeed -= 0.5f;
                        }
                    }
                }  

                characterManager.MyUpdate();
                atbTimer.MyUpdate();
                uiManager.MyUpdate();
            }   
        }
        else // 編輯 
        {
            if (startDelay > 0)
            {
                uiManager.BattleUI((startDelay - 1.0f) / 1.0f);

                startDelay -= Time.deltaTime;
                if (startDelay <= 0) uiManager.blocksAnimator.SetTrigger("hide");
            }
            else {
                // IF指到方塊群組方塊
                Vector2 nowGrid = new Vector2(-1, -1);
                List<BlockObj> blockObjs = new List<BlockObj>();
                foreach (RaycastResult hit in uiRayCast.Raycast())
                {
                    if (hit.gameObject.name == "program") nowGrid = CalculateGrid();
                    BlockObj blockObj = hit.gameObject.GetComponent<BlockObj>();
                    if (blockObj != null) blockObjs.Add(blockObj);
                }
                if (blockObjs.Count >= 1 && uiManager.nowDragBlock == null) uiManager.UpdateDescription(blockObjs[0].block);
                else uiManager.HideDescription();

                //拖曳方塊判斷
                if (blockObjs.Count == 1)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (nowGrid != new Vector2(-1, -1)) removeBlock(nowGrid);
                        uiManager.nowDragBlock = blockObjs[0].block;
                        uiManager.dragBlock.SetActive(true);
                    }
                }
                if (uiManager.nowDragBlock != null)
                {
                    if (Input.GetMouseButton(0))
                    {
                        uiManager.UpdateDragBlock(uiManager.nowDragBlock);
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        SetBlock(nowGrid, uiManager.nowDragBlock);
                        uiManager.dragBlock.SetActive(false);
                        uiManager.nowDragBlock = null;
                    }
                }
            
            } 
        }
    }

    ////////////////////////////////////////////////////////////
    /// editor function
    ////////////////////////////////////////////////////////////
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
        int inBlockIndex = -1;
        int flag = checkOverlap(nowGrid, block, -1, ref inBlockIndex);  
        // 超過最底
      
        // 更改 memory  更新UI
        if (flag != 0) {
            Block cloneBlock = new Block();
            if (block.GetType() == typeof(AttackBlock)) cloneBlock = new AttackBlock((AttackBlock)block);
            else if (block.GetType() == typeof(BuffBlock)) cloneBlock = new BuffBlock((BuffBlock)block);
            else if (block.GetType() == typeof(IFBlock)) cloneBlock = new IFBlock((IFBlock)block);
            else if (block.GetType() == typeof(MultipleBlock)) cloneBlock = new MultipleBlock((MultipleBlock)block);

            if (flag == 1) characterManager.characters[(int)nowGrid.x].process[(int)nowGrid.y] = cloneBlock;
            else {
                BlockDecorator tempBlock = new BlockDecorator((BlockDecorator)characterManager.characters[(int)nowGrid.x].process[inBlockIndex]);
                tempBlock.SetIn(cloneBlock);
                int garbage = 0;
                int flag2 = checkOverlap(new Vector2(nowGrid.x, inBlockIndex), tempBlock, inBlockIndex, ref garbage); 

                if (flag2 == 1) ((BlockDecorator)characterManager.characters[(int)nowGrid.x].process[inBlockIndex]).SetIn(cloneBlock); 
            }
            uiManager.UpdateMemory();
        }
    }
    // 0 不存  // 1 存  // 2 in
    int checkOverlap(Vector2 nowGrid,Block block,int ignore,ref int blockIndex) {
        int nowStart = (int)nowGrid.y;
        int nowEnd = (int)nowGrid.y + block.GetCast() - 1;

        if (nowEnd >= characterManager.TotalMemory) return 0;
        if (nowGrid == new Vector2(-1, -1)) return 0 ;

        int flag = 1;
        for (int i = 0; i < characterManager.TotalMemory; i++)
        {
            Block exist = characterManager.characters[(int)nowGrid.x].process[i];
            if (exist.name != null)
            {
                int existStart = i;
                int existEnd = i + exist.GetCast() - 1;
                if (existStart <= nowEnd && existEnd >= nowStart && i != ignore)
                {
                    if (typeof(BlockDecorator).IsAssignableFrom(exist.GetType())) {
                        blockIndex = i;
                        flag = 2;
                    }
                    else flag = 0;
                    break;
                }
            }
        }
        return flag;
    }
    Vector2 CalculateGrid() {
        Vector2 programC = new Vector2(Input.mousePosition.x - 1280, Input.mousePosition.y);
        int x = (int)(programC.x / (640.0f / 3.0f)); 
        int y = (int)(programC.y / (1020.0f / (float)characterManager.TotalMemory));
        Vector3 re = new Vector2(x, characterManager.TotalMemory - y -1); 
        return re;
    }
    ////////////////////////////////////////////////////////////
    /// button event
    ////////////////////////////////////////////////////////////
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
    public void StartButton()
    {
        if (0 < startDelay && startDelay < 3) return;
        start = !start;
        if (start){
            startDelay = 0.0f;
            uiManager.startButtonText.text = "撤退";
        }
        else{
            startDelay = 2.0f;
            uiManager.startButtonText.text = "出擊";
        }
        characterManager.characters[3].process = stageMonster.getProcess(0);
        characterManager.resetGame();
        atbTimer.ReStart();
        uiManager.resetGame();
        uiManager.MyUpdate();
    }

}
