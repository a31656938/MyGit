using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuffBlock : Block
{
    public bool benefit;

    public BuffBlock(string name, string description, int cast, bool benefit)
    {
        BaseConstructor(name, description, cast);
        this.benefit = benefit;
    }
}
