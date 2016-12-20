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
    public Transform IFParent;
    public Transform multipleParent;
    public List<AttackBlock> attackBlockGroup;
    public List<BuffBlock> buffBlockGroup;
    public List<IFBlock> IFBlockGroup;
    public List<MultipleBlock> MultipleBlockGroup;
    public List<int> panelHeight;
    float offset = 0;
	// Use this for initialization
	public void Initial () {
        Buff tempBuff;

        panelHeight = new List<int>();
        offset = 0;
        attackBlockGroup = new List<AttackBlock>();
        tempBuff = new Buff("oiled", "油油的", 3);
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("Oil", "被油瓶砸到還是會痛的，身體還會油油的", 2, tempBuff, 5)));
        tempBuff = new Buff("fired", "燃燒吧", 3);
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("FireBall", "隨處可見的火球術", 3, tempBuff, 20)));
        tempBuff = new Buff("wet", "潮", 3);
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("WaterGun", "強勁的噴射水槍攻擊", 4, tempBuff, 30)));
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("WindSword", "徐徐吹來的風刃", 4, null, 10)));
        panelHeight.Add((int)Mathf.Abs(offset - 10));

        offset = 0;
        buffBlockGroup = new List<BuffBlock>();
        tempBuff = new Buff("powerUp", "力量加倍", 10);
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("PowerUp", "感覺全身充滿了力氣", 3, tempBuff, true)));
        tempBuff = new Buff("powerDown", "力量減半", 10);
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("PowerDown", "感覺全身一點力氣也沒有", 4, tempBuff, false)));
        tempBuff = new Buff("speedUp", "速度加倍", 10);
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("SpeedUp", "獲得風馳電掣般的速度", 3, tempBuff, true)));
        panelHeight.Add((int)Mathf.Abs(offset - 10));

        offset = 0;
        IFBlockGroup = new List<IFBlock>();
        IFBlockGroup.Add((IFBlock)CreateBlock(new IFBlock("如果0000", "0000000", 1,null)));
        IFBlockGroup.Add((IFBlock)CreateBlock(new IFBlock("如果1111", "1111111", 2, null)));
        panelHeight.Add((int)Mathf.Abs(offset - 10));

        offset = 0;
        MultipleBlockGroup = new List<MultipleBlock>();
        MultipleBlockGroup.Add((MultipleBlock)CreateBlock(new MultipleBlock("重作0000", "0000000", 1, null)));
        MultipleBlockGroup.Add((MultipleBlock)CreateBlock(new MultipleBlock("重作1111", "1111111", 2, null)));
        panelHeight.Add((int)Mathf.Abs(offset - 10));

    }

    Block CreateBlock(Block block){
        // 決定父物件
        Transform parent = attackParent;
        float Y = 1020.0f / (float)GameManager.Inst.characterManager.TotalMemory;

        if (block.GetType() == typeof(AttackBlock)) parent = attackParent;
        else if (block.GetType() == typeof(BuffBlock)) parent = buffParent;
        else if (block.GetType() == typeof(IFBlock)) parent = IFParent;
        else if (block.GetType() == typeof(MultipleBlock)) parent = multipleParent;
        // 生物件 暫存component
        //初始設定
        offset -= 10;
        Vector3 pos = new Vector3(60,offset,0);
        GameManager.Inst.uiManager.showBlockUI(block, parent, pos);

        offset -= Y * block.GetCast();
        return block;
    }
    public void setAllActive(bool set) {
        foreach (Transform child in nowBlocks) {
            child.gameObject.SetActive(set);
        }
    }
}
