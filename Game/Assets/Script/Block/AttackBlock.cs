using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackBlock : Block {
    public int attack;
    
    public AttackBlock(string name, string description,int attack) {
        this.name = name;
        this.description = description;
        this.attack = attack;
    }
}
