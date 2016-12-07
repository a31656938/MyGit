using UnityEngine;
using System.Collections;

public abstract class Buff {
    public string name;
    public string description;
    public float timer;

    public Buff(string name,string description ,float timer) {
        this.name = name;
        this.description = description;
        this.timer = timer;
    }
    abstract public void Modify(ref Character character);
    public bool Finish() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            return true;
        }
        return false;
    }
}
