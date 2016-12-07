using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block {
    public string name;
    public string description;
    public Sprite icon;
    public int idle;

    public void BaseConstructor(string name, string description, int idle) {
        this.name = name;
        this.description = description;
        this.idle = idle;
        icon = Resources.Load<Sprite>("BlockIcon/" + name);
    }

}
