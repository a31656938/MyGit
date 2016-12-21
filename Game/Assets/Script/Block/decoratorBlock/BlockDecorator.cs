using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlockDecorator : Block {
    public Block inBlock;

    public BlockDecorator() { }
    public BlockDecorator(BlockDecorator block) {
        SetBase(block.name, block.description, block.cast, block.color, block.inBlock);
    }

    public void SetBase(string name, string description, int cast,Color color,Block inBlock) {
        base.SetBase(name, description, cast, "", color, null); 
        this.inBlock = inBlock;
    }
    public void SetIn(Block block) {
        inBlock = block;
    }
    public int GetLocalCast() {
        return this.cast;
    }
    public override int GetCast()
    {
        if (inBlock == null) return this.cast;
        else return inBlock.GetCast() + this.cast;
    }
}


