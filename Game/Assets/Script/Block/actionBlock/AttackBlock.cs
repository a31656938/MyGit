using UnityEngine;
using System.Collections;

[System.Serializable]
public class AttackBlock : Block {
    public int attack;

    public AttackBlock(string name, string description, int cast, int attack):base(name,description,cast)
    {
        this.attack = attack;
        this.color = new Color(1, 0.2f, 0.2f);
    }
}
