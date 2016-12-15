using UnityEngine;
using System.Collections;

[System.Serializable]
public class IFBlock : BlockDecorator
{
    public IFBlock(IFBlock block) {
        SetBase(block.name, block.description, block.cast, new Color(1, 1, 0), block.inBlock);
    }
    public IFBlock(string name, string description, int cast,Block inBlock) {
        SetBase(name, description, cast, new Color(1, 1, 0), inBlock);
    }
}
