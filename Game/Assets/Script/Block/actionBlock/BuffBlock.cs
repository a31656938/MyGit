using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuffBlock : Block
{
    public bool benefit;

    public BuffBlock(string name, string description, int cast, bool benefit):base(name,description,cast)
    {
        this.benefit = benefit;
        this.color = new Color(0.2f, 1, 0.2f);
    }
}
