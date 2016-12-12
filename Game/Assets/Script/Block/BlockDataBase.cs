using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[System.Serializable]
public class BlockDataBase : MonoBehaviour{
    public GameObject emptyBlock;
    public Transform nowBlocks;
    public Transform attackParent;
    public Transform buffParent;
    public List<AttackBlock> attackBlockGroup;
    public List<BuffBlock> buffBlockGroup;
    public List<int> panelHeight;
    float offset = 0;
	// Use this for initialization
	public void Initial () {
        panelHeight = new List<int>();
        offset = 0;
        //attackBlockGroup = new List<AttackBlock>();
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("Oil", "被油瓶砸到還是會痛的，身體還會油油的", 2, 5)));
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("FireBall", "隨處可見的火球術", 3, 20)));
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("WaterGun", "強勁的噴射水槍攻擊", 4, 30)));
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("WindSword", "徐徐吹來的風刃", 4, 10)));
        panelHeight.Add((int)Mathf.Abs(offset - 10));

        offset = 0;
        buffBlockGroup = new List<BuffBlock>();
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("PowerUp", "感覺全身充滿了力氣", 3, true)));
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("PowerDown", "感覺全身一點力氣也沒有", 4, false)));
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("SpeedUp", "獲得風馳電掣般的速度", 3, true)));
        panelHeight.Add((int)Mathf.Abs(offset - 10));

    }

    Block CreateBlock(Block block){
        // 決定父物件
        Transform parent = attackParent;
        if(block.GetType() == typeof(AttackBlock)) parent = attackParent;
        else if (block.GetType() == typeof(BuffBlock)) parent = buffParent;
        // 生物件 暫存component
        GameObject temp = (GameObject)GameObject.Instantiate(emptyBlock, parent, false);
        BlockObj blockObj = temp.AddComponent<BlockObj>();
        //初始設定
        blockObj.block = block;
        offset -= 10;
        float Y = 1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;
        temp.GetComponent<RectTransform>().sizeDelta = new Vector2(180, Y * block.cast);
        temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(60,offset,0);
        offset -= Y * block.cast;

        temp.transform.GetChild(0).GetComponent<Image>().sprite = block.icon;
        temp.name = block.name;
        return block;
    }
    public void setAllActive(bool set) {
        foreach (Transform child in nowBlocks) {
            child.gameObject.SetActive(set);
        }
    }
}
