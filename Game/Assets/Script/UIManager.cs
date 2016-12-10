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
    public void UpdateDragBlock(Block block) {
        float Y = 1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;

        dragBlock.GetComponent<RectTransform>().anchoredPosition3D = Input.mousePosition;
        dragBlock.GetComponent<RectTransform>().sizeDelta = new Vector2(180, Y * block.cast);
        dragBlock.transform.GetChild(0).GetComponent<Image>().sprite = block.icon;
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
}
