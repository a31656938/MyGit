using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ATBUI : MonoBehaviour {
    public float perctange;
    private float barWidth;
    public List<RectTransform> atbIcons;
    public ATBTimer atbTimer;
    public Image bar;

    public GameObject atbIconPrefab;
	// Use this for initialization
	public void Initial () {
        foreach (Character character in GameManager.Inst.characterManager.characters){
            GameObject temp = (GameObject)Instantiate(atbIconPrefab, bar.transform, false);
            Image tempImage = temp.GetComponent<Image>();
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();

            tempImage.sprite = character.atbIcon;
            atbIcons.Add(tempRectTransform);
        }
        atbTimer = GameManager.Inst.atbTimer;
        barWidth = bar.gameObject.GetComponent<RectTransform>().sizeDelta.x;
	}

    // Update is called once per frame
    public void MyUpdate(){
        for (int i = 0; i < atbIcons.Count; i++) {
            ATBCharacter nowData = atbTimer.atbs[i];
            float t = positionT(nowData, this.perctange);
            atbIcons[i].anchoredPosition3D = new Vector3(barWidth * t, 0, 0); 
        }
    }
    float positionT(ATBCharacter nowData , float perctange){
        float t = 0;
        if (nowData.IsIdle){
            t = (1 - nowData.nowTime / atbTimer.maxIdleTime) * perctange;
        }
        else {
            t = perctange + (1 - perctange) * (1 - nowData.nowTime / nowData.castTime);
        }
        return t;
    }
}
