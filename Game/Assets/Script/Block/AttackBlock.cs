using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackBlock : Block {
    public int attack;

    public AttackBlock(string name, string description, int cast, int attack)
    {
        BaseConstructor(name, description, cast);
        this.attack = attack;
    }
}
