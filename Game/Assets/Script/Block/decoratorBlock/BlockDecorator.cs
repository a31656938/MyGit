using UnityEngine;
using System.Collections;

public class BlockDecorator : Block {
    public Block inBlock;

    public BlockDecorator(string name, string description, int cast,Block inBlock):base(name,description,cast) {
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


