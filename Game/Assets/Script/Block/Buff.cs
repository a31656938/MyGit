using UnityEngine;
using System.Collections;

[System.Serializable]
public class Buff {
    public string name;
    public string description;
    public Sprite icon;
    public int time;

    public Buff(string name, string description, int time) {
        this.name = name;
        this.description = description;
        this.time = time;
        icon = Resources.Load<Sprite>("BuffIcon/" + name);
    }
}
