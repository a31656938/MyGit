using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class States  {
    public int maxHP;
    public int nowHP;
    public List<Buff> buffs;

    public Buff HaveBuff(string name) {
        foreach (Buff b in buffs) {
            if(b.name == name) return b;
        }
        return null;
    }
    public int CountBuff(string name){
        int re = 0;
        foreach (Buff b in buffs){
            if (b.name == name) re++;
        }
        return re;
    }
}
