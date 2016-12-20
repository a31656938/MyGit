using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuffBlock : Block
{
    public bool benefit;

    public BuffBlock(BuffBlock block) {
        SetBase(block.name, block.description, block.cast, new Color(0.2f, 1, 0.2f),block.buff);
        this.benefit = block.benefit;
    }
    public BuffBlock(string name, string description, int cast, Buff buff, bool benefit){
        SetBase(name, description, cast, new Color(0.2f, 1, 0.2f),buff);
        this.benefit = benefit;
    }
}
