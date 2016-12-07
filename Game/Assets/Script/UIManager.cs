using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    public ATBUI atbUI;
    public Transform program;
    public GameObject gridPrefab;

	// Use this for initialization
	public void Initial () {
        atbUI = this.GetComponent<ATBUI>();

        CreateGrid();
       

            atbUI.Initial();
	}
	
	// Update is called once per frame
	public void MyUpdate () {
        atbUI.MyUpdate();
	}
    void CreateGrid() {
        // grid row;
        float offsetY = -1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;
        for (int i = 1; i <= GameManager.Inst.characterManager.TotalMemory - 1; i++)
        {
            GameObject temp = (GameObject)Instantiate(gridPrefab, program, false);
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            tempRectTransform.sizeDelta = new Vector2(640, 5);
            tempRectTransform.anchoredPosition3D = new Vector3(0, offsetY * i, 0);
        }
        // grid column
        float offsetX = 640 / 3;
        for (int i = 1; i <= 2; i++)
        {
            GameObject temp = (GameObject)Instantiate(gridPrefab, program, false);
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            tempRectTransform.sizeDelta = new Vector2(5, 1020);
            tempRectTransform.anchoredPosition3D = new Vector3(offsetX * i, 0, 0);
        }
    }
}
