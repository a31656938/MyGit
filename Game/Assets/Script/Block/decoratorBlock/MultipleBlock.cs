using UnityEngine;
using System.Collections;

public class MultipleBlock : BlockDecorator
{
    public MultipleBlock(string name, string description, int cast, Block inBlock)
        : base(name, description, cast, inBlock)
    {
        color = new Color(0, 0.2f, 1);
    }
}
