using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackBlock : Block {
    public int attack;
    
    public AttackBlock(string name, string description,int idle ,int attack) {
        BaseConstructor(name, description, idle);
        this.attack = attack;
    }
}
