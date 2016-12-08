using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block {
    public string name;
    public string description;
    public Sprite icon;
    public int cast;

    public void BaseConstructor(string name, string description, int cast)
    {
        this.name = name;
        this.description = description;
        this.cast = cast;
        icon = Resources.Load<Sprite>("BlockIcon/" + name);
    }
    public string GetDescription() {
        return name + "\n" + description;
    }

}
