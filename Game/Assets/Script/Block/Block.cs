using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block {
    public string name;
    public string description;
    public Sprite icon;
    public Color color;
    protected int cast;
    private string name1;
    private string description1;
    private int cast1;

    public Block() { }
    public Block(Block block) {
        this.name = block.name;
        this.description = block.description;
        this.cast = block.cast;
        icon = Resources.Load<Sprite>("BlockIcon/" + block.name);
    }
    public Block(string name, string description, int cast)
    {
        this.name = name;
        this.description = description;
        this.cast = cast;
        icon = Resources.Load<Sprite>("BlockIcon/" + name);
    }
    public string GetDescription() {
        return description;
    }
    virtual public int GetCast(){
        return this.cast;
    }
}
