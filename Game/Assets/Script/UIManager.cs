using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    public ATBUI atbUI;
    public Text nowGroupName;
    public Text startButtonText;
    public Transform program;
    public Animator blocksAnimator;
    public float BlockY = 0;
    public float BlockX = 0;
    public GameObject blockPrefab;
    // 拖曳方塊
    public GameObject dragBlock;
    public Block nowDragBlock;
    // 描述
    public GameObject description;
    float descriptionTimer;
    // 排版線
    public Transform girdParent;
    public GameObject gridPrefab;
    // 腳色狀態
    public GameObject playerStates;
    public GameObject monsterBuffs;
    public GameObject buffPrefab;
    // 時間縣
    public Transform timeLineParent;
    public GameObject timeLinePrefab;
    // hit effect
    public Transform hitEffectParent;
    public List<Animator> animators;

	// Use this for initialization
	public void Initial () {
        atbUI = this.GetComponent<ATBUI>();
        startButtonText.text = "出擊";
        dragBlock.SetActive(false);
        description.SetActive(false);
        BlockY = 1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;
        BlockX = 640.0f / 3.0f;

        animators = new List<Animator>();
        foreach (Transform child in hitEffectParent) {
            animators.Add(child.GetComponent<Animator>());
        }

        CreateGrid();   

        atbUI.Initial();
	}

	// Update is called once per frame
	public void MyUpdate () {
        foreach (Transform child in timeLineParent){
            Destroy(child.gameObject);
        }
        atbUI.MyUpdate();


        // timeLine
        for (int i = 0; i < GameManager.Inst.characterManager.characters.Count - 1; i++) {
            ATBCharacter c = GameManager.Inst.characterManager.characters[i].atb;
            GameObject temp = (GameObject)Instantiate(timeLinePrefab, timeLineParent, false);
            temp.GetComponent<Image>().color = GameManager.Inst.characterManager.characters[i].color;
            if (c.IsIdle){
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(BlockX * i, -60, 0);
            }
            else {                
                float t = c.castTime - c.nowTime;
                float y = -60 - t * BlockY;
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(BlockX * i, y, 0);
            }
        }

	}
    public void resetGame() {
        atbUI.MyUpdate();
        UpdateStates();
    }
    public void showBlockUI(Block block, Transform parent, Vector3 position)
    {
        showBlockUI(block, parent, position, 0);
    }
    public void showBlockUI(Block block,Transform parent,Vector3 position,int layer) { 
        GameObject temp = (GameObject)Instantiate(blockPrefab, parent, false);
        temp.name = block.name;
        temp.GetComponent<RectTransform>().anchoredPosition3D = position;
        temp.GetComponent<RectTransform>().sizeDelta = new Vector2(180 - 20 * layer, BlockY * block.GetCast() - 0.2f * BlockY * layer);
        temp.GetComponent<Image>().color = block.color;
        temp.GetComponent<BlockObj>().block = block;

        if (typeof(BlockDecorator).IsAssignableFrom(block.GetType())){
            temp.transform.GetChild(0).gameObject.SetActive(false);
            temp.transform.GetChild(1).GetComponent<Text>().text = block.name;
            temp.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(180 - 20 * layer, BlockY * ((BlockDecorator)block).GetLocalCast() - 0.2f * BlockY * layer);
            // 判斷是否有合法inBlock
            if(((BlockDecorator)block).inBlock != null){
                if (((BlockDecorator)block).inBlock.name != null) {
                    float tempY = (-1 * ((BlockDecorator)block).GetLocalCast() - 0.1f) * BlockY;
                    showBlockUI(((BlockDecorator)block).inBlock, temp.transform, new Vector3(10, tempY, 0), layer + 1); 
                }
            }  
        }
        else {
            temp.transform.GetChild(0).GetComponent<Image>().sprite = block.icon;
            temp.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void HideDescription() {
        descriptionTimer = 0;
        description.SetActive(false);
    }
    public void UpdateDescription(Block block) {
        descriptionTimer += Time.deltaTime;
        if (block.name != description.transform.GetChild(0).GetComponent<Text>().text) HideDescription();
        else if (description.GetComponent<RectTransform>().anchoredPosition3D != Input.mousePosition) HideDescription();
        else if (descriptionTimer >= 0.7f) description.SetActive(true);

        description.GetComponent<RectTransform>().anchoredPosition3D = Input.mousePosition;
        description.transform.GetChild(0).GetComponent<Text>().text = block.name;
        description.transform.GetChild(1).GetComponent<Text>().text = block.description;
        
    }
    public void UpdateDragBlock(Block block)
    {
        foreach (Transform child in dragBlock.transform)
        {
            Destroy(child.gameObject);
        }

        dragBlock.GetComponent<RectTransform>().anchoredPosition3D = Input.mousePosition;
        showBlockUI(block, dragBlock.transform, Vector3.zero);
    }
    public void UpdateStates() {
        float t = (float)((float)GameManager.Inst.characterManager.states[0].nowHP / (float)GameManager.Inst.characterManager.states[0].maxHP);
        playerStates.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(450.0f * t, 30);

        // update Buffs
        foreach (Transform child in monsterBuffs.transform){
            Destroy(child.gameObject);
        }
        foreach (Transform child in playerStates.transform.GetChild(1)) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i <= 1; i++){
            float x = 10;
            Transform parent;
            if (i == 0) parent = playerStates.transform.GetChild(1);
            else parent = monsterBuffs.transform;

            foreach (Buff buff in GameManager.Inst.characterManager.states[i].buffs){
                GameObject temp = (GameObject)Instantiate(buffPrefab, parent, false);
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(x, 0, 0);
                temp.GetComponent<Image>().sprite = buff.icon;
                x += 70;
            }
        }


    }


    public void HitEffect(int characterIndex, string name) {
        GameManager.Inst.characterManager.characterObjs[characterIndex].transform.GetChild(1).GetComponent<Animator>().SetTrigger("hit");
        animators[characterIndex].SetTrigger(name);
    }
    public void BattleUI(float t)
    {
        t = Mathf.Clamp01(t);
        
        atbUI.BattleUI(t);
        for (int i = 0; i < 3; i++) {
            // 腳色位置
            Transform temp = GameManager.Inst.characterManager.characterObjParent.GetChild(i).GetChild(0);
            float y = 0;
            switch (i) {
                case 0: y = 0; break;
                case 1: y = 0.6f; break;
                case 2: y = -0.6f; break;
            }
            Vector3 dir = new Vector3(-1, y, 0).normalized;
            temp.localPosition = dir * t * 2f; 
           
        }
        // states
        playerStates.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(0, 0.5f, t));
        playerStates.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 0, 0, t);
        playerStates.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 1, 0, t);
        playerStates.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(0, 0.5f, t));
        monsterBuffs.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(0, 0.5f, t)); ;
    }
    void CreateGrid() {
        // grid row;
        for (int i = 1; i <= GameManager.Inst.characterManager.TotalMemory - 1; i++)
        {
            GameObject temp = (GameObject)Instantiate(gridPrefab, girdParent, false);
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            tempRectTransform.sizeDelta = new Vector2(640, 5);
            tempRectTransform.anchoredPosition3D = new Vector3(0, -1 * BlockY * i, 0);
        }
        // grid column
        for (int i = 1; i <= 2; i++)
        {
            GameObject temp = (GameObject)Instantiate(gridPrefab, girdParent, false);
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            tempRectTransform.sizeDelta = new Vector2(5, 1020);
            tempRectTransform.anchoredPosition3D = new Vector3(BlockX * i, 0, 0);
        }
    }
    public void UpdateMemory() {
        foreach (Transform child in program) {
            Destroy(child.gameObject);
        }
        float Xoffset = (BlockX - 180.0f) / 2.0f;

        for (int i = 0; i < GameManager.Inst.characterManager.characters.Count - 1; i++) {
            for (int j = 0; j < GameManager.Inst.characterManager.TotalMemory; j++) {
                Block tempBlock = GameManager.Inst.characterManager.characters[i].process[j];
                if (tempBlock.name != null){
                    Vector3 pos = new Vector3(BlockX * i + Xoffset, -1 * BlockY * j, 0);
                    showBlockUI(tempBlock, program, pos);
                    
                }
            }
        }
    }
}
