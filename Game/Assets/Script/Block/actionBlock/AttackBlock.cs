using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackBlock : Block {
    public int attack;

    public AttackBlock(AttackBlock block){
        SetBase(block.name, block.description, block.cast, new Color(1, 0.2f, 0.2f),block.buff);
        this.attack = block.attack;
    }
    public AttackBlock(string name, string description, int cast,Buff buff, int attack){
        SetBase(name, description, cast, new Color(1, 0.2f, 0.2f), buff);  
        this.attack = attack;
    }
}
