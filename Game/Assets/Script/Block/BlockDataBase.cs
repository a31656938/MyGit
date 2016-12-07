using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[System.Serializable]
public class BlockDataBase : MonoBehaviour {
    public List<Block> nowblockGroup;
    public List<Block> attackBlockGroup;
	// Use this for initialization
	public void Initial () {
        nowblockGroup = new List<Block>();
        attackBlockGroup.Add(new AttackBlock("attack01", "This is attack01", 10));
        attackBlockGroup.Add(new AttackBlock("attack02", "This is attack02", 20));
        attackBlockGroup.Add(new AttackBlock("attack03", "This is attack03", 30));
        nowblockGroup = attackBlockGroup;
	}
	
	// Update is called once per frame
	public void MyUpdate () {
	
	}
}
