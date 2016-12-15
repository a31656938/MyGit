using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block {
    public string name;
    public string description;
    public Sprite icon;
    public Color color;
    protected int cast;

    public Block() { }
    public void SetBase(string name, string description, int cast,Color color)
    {
        this.name = name;
        this.description = description;
        this.cast = cast;
        icon = Resources.Load<Sprite>("BlockIcon/" + name);
        this.color = color;
    }
    public string GetDescription() {
        return description;
    }
    virtual public int GetCast(){
        return this.cast;
    }
}
