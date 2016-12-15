using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackBlock : Block {
    public int attack;

    public AttackBlock(AttackBlock block){
        SetBase(block.name, block.description, block.cast, new Color(1, 0.2f, 0.2f));
        this.attack = block.attack;
    }
    public AttackBlock(string name, string description, int cast, int attack){
        SetBase(name, description, cast, new Color(1, 0.2f, 0.2f)); 
        this.attack = attack;
    }
}
