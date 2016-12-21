using UnityEngine;
using System.Collections;

[System.Serializable]
public class Buff {
    public string name;
    public string description;
    public Sprite icon;
    public int count;
    public float timer;
    public Buff(string name, string description, int count){
        this.name = name;
        this.description = description;
        this.count = count;
        icon = Resources.Load<Sprite>("BuffIcon/" + name);
        timer = 0;
    }
}
