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
	// Use this for initialization
	public void Initial () {
        attackBlockGroup = new List<AttackBlock>();
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("Oil", "丟擲油瓶", 2, 5))); 
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("FireBall", "一般的火球術", 3, 20)));
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("WaterGun", "水槍攻擊", 4, 30))); 
        attackBlockGroup.Add((AttackBlock)CreateBlock(new AttackBlock("WindSword", "風刃", 4, 10)));

        buffBlockGroup = new List<BuffBlock>();
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("PowerUp", "力量強化", 3, true)));
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("PowerDown", "力量弱化", 4, false)));
        buffBlockGroup.Add((BuffBlock)CreateBlock(new BuffBlock("SpeedUp", "速度加快", 3, true)));
	}

    Block CreateBlock(Block block){
        Transform parent = attackParent;
        if(block.GetType() == typeof(AttackBlock)) parent = attackParent;
        else if (block.GetType() == typeof(BuffBlock)) parent = buffParent;

        GameObject temp = (GameObject)GameObject.Instantiate(emptyBlock, parent, false);
        BlockObj blockObj = temp.AddComponent<BlockObj>();
        
        blockObj.block = block;
        temp.GetComponent<Image>().sprite = block.icon;
        temp.name = block.name;
        return block;
    }
    public void setAllActive(bool set) {
        foreach (Transform child in nowBlocks) {
            child.gameObject.SetActive(set);
        }
    }
}
