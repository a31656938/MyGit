using UnityEngine;
using System.Collections;

public class IFBlock : BlockDecorator
{
    public IFBlock(string name, string description, int cast,Block inBlock) 
        : base(name,description,cast,inBlock){
            color = new Color(1, 0.2f, 0); 
    }
}
