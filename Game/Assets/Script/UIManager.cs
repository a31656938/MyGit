using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    public ATBUI atbUI;
    public Text nowGroupName;
    public Text startButtonText;
    public Transform program;
    public Transform girdParent;
    public GameObject gridPrefab;
    public Animator blocksAnimator;

    public GameObject blockPrefab;
    public GameObject dragBlock;

    public GameObject description;
    public float descriptionTimer;
	// Use this for initialization
	public void Initial () {
        atbUI = this.GetComponent<ATBUI>();
        startButtonText.text = "出擊";
        dragBlock.SetActive(false);
        description.SetActive(false);

        CreateGrid();   

        atbUI.Initial();
	}

	// Update is called once per frame
	public void MyUpdate () {
        atbUI.MyUpdate();
	}
    public void showBlockUI(Block block, Transform parent, Vector3 position)
    {
        showBlockUI(block, parent, position, 0);
    }
    public void showBlockUI(Block block,Transform parent,Vector3 position,int layer) { 
        float Y = 1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;

        GameObject temp = (GameObject)Instantiate(blockPrefab, parent, false);
        temp.name = block.name;
        temp.GetComponent<RectTransform>().anchoredPosition3D = position;
        temp.GetComponent<RectTransform>().sizeDelta = new Vector2(180 - 20 * layer, Y * block.GetCast() - 0.2f * Y * layer);
        temp.GetComponent<Image>().color = block.color;
        temp.GetComponent<BlockObj>().block = block;

        if (typeof(BlockDecorator).IsAssignableFrom(block.GetType())){
            temp.transform.GetChild(0).gameObject.SetActive(false);
            if(((BlockDecorator)block).inBlock != null){
                float tempY = (-1 * ((BlockDecorator)block).GetLocalCast() - 0.1f) * Y; 
                showBlockUI(((BlockDecorator)block).inBlock, temp.transform, new Vector3(10, tempY, 0), layer + 1); 
            }  
        }
        else temp.transform.GetChild(0).GetComponent<Image>().sprite = block.icon;
    }
    public void UpdateDragBlock(Block block) {
        foreach (Transform child in dragBlock.transform){
            Destroy(child.gameObject);
        } 

        dragBlock.GetComponent<RectTransform>().anchoredPosition3D = Input.mousePosition;
        showBlockUI(block, dragBlock.transform, Vector3.zero);
    }
    public void HideDescription() {
        descriptionTimer = 0;
        description.SetActive(false);
    }
    public void ChangeDescription(Block block) {
        descriptionTimer += Time.deltaTime;
        if (block.name != description.transform.GetChild(0).GetComponent<Text>().text) HideDescription();
        else if (description.GetComponent<RectTransform>().anchoredPosition3D != Input.mousePosition) HideDescription();
        else if (descriptionTimer >= 0.7f) description.SetActive(true);

        description.GetComponent<RectTransform>().anchoredPosition3D = Input.mousePosition;
        description.transform.GetChild(0).GetComponent<Text>().text = block.name;
        description.transform.GetChild(1).GetComponent<Text>().text = block.description;
        
    }
    void CreateGrid() {
        // grid row;
        float offsetY = -1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;
        for (int i = 1; i <= GameManager.Inst.characterManager.TotalMemory - 1; i++)
        {
            GameObject temp = (GameObject)Instantiate(gridPrefab, girdParent, false);
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            tempRectTransform.sizeDelta = new Vector2(640, 5);
            tempRectTransform.anchoredPosition3D = new Vector3(0, offsetY * i, 0);
        }
        // grid column
        float offsetX = 640 / 3;
        for (int i = 1; i <= 2; i++)
        {
            GameObject temp = (GameObject)Instantiate(gridPrefab, girdParent, false);
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            tempRectTransform.sizeDelta = new Vector2(5, 1020);
            tempRectTransform.anchoredPosition3D = new Vector3(offsetX * i, 0, 0);
        }
    }
    public void UpdateMemory() {
        foreach (Transform child in program) {
            Destroy(child.gameObject);
        }
        float Y = 1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;
        float X = (640.0f / 3.0f);
        float Xoffset = (X - 180.0f) / 2.0f;

        for (int i = 0; i < GameManager.Inst.characterManager.characters.Count; i++) {
            for (int j = 0; j < GameManager.Inst.characterManager.TotalMemory; j++) {
                Block tempBlock = GameManager.Inst.characterManager.characters[i].process[j];
                if (tempBlock.name != null){
                    Vector3 pos = new Vector3(X * i + Xoffset, -1 * Y * j, 0);
                    showBlockUI(tempBlock, program, pos);
                    
                }
            }
        }
    }
}
