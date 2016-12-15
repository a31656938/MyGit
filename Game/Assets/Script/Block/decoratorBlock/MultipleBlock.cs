using UnityEngine;
using System.Collections;

[System.Serializable]
public class MultipleBlock : BlockDecorator
{
    public MultipleBlock(MultipleBlock block) {
        SetBase(block.name, block.description, block.cast, new Color(0, 0.2f, 1), block.inBlock);
    }
    
    public MultipleBlock(string name, string description, int cast, Block inBlock) {
        SetBase(name, description, cast, new Color(0, 0.2f, 1), inBlock);
    }
}
