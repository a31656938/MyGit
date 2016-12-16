using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ATBTimer : MonoBehaviour {
    public float maxIdleTime;
    public List<ATBCharacter> atbs;

    public void Initial(float maxIdleTime) { 
        this.maxIdleTime = maxIdleTime;
        this.atbs = new List<ATBCharacter>();

        foreach (Character character in GameManager.Inst.characterManager.characters) {
            ATBCharacter atb = character.atb;
            atbs.Add(atb);
        }

    }
    public void MyUpdate() {
        foreach (ATBCharacter atb in atbs) {
            atb.nowTime -= Time.deltaTime * atb.nowSpeed;
            if (atb.nowTime < 0) atb.change();
        }
    }
    public void ReStart() {
        foreach (ATBCharacter atb in atbs) {
            maxIdleTime = Mathf.Max(atb.idleTime, maxIdleTime);
            atb.nowTime = atb.idleTime;
            atb.IsIdle = true;
        }
    }
}

[System.Serializable]
public class ATBCharacter {
    public bool IsIdle;
    public int nowCast;

    public float idleTime;
    public float castTime;
    public float nowSpeed;
    public float nowTime;

    public ATBCharacter(float nowSpeed,float idleTime,float castTime) {
        this.nowSpeed = nowSpeed;
        this.idleTime = idleTime;
        this.castTime = castTime;
    }
    public void Set(float nowSpeed,float idleTime,float castTime) {
        this.nowSpeed = nowSpeed;
        this.idleTime = idleTime;
        this.castTime = castTime;
    }
    public void change() {
        if (this.IsIdle) this.nowTime = this.castTime;
        else this.nowTime = this.idleTime;

        nowCast = -1;
        this.IsIdle = !this.IsIdle;
    }
}
