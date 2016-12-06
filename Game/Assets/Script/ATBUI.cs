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
        foreach (Character character in GameManager.Inst.characters){
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
        float bound = barWidth * perctange;
        for (int i = 0; i < atbIcons.Count; i++) {
            ATBCharacter nowData = atbTimer.atbs[i];
            float position = 0;
            if (nowData.IsIdle){
                position = (1 - nowData.nowTime / atbTimer.maxIdleTime) * bound;
            }
            else {
                position = bound + (barWidth - bound) * (1 - nowData.nowTime / nowData.castTime);
            }
            atbIcons[i].anchoredPosition3D = new Vector3(position, 0, 0);
        }
    }
}
