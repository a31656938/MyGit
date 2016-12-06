using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ATBTimer : MonoBehaviour {
    public float maxIdleTime;
    public float maxCastTime;
    public List<ATBCharacter> atbs;

    public void Initial(float maxIdleTime,float maxCastTime ,ref List<ATBCharacter> atbs) { 
        this.maxIdleTime = maxIdleTime;
        this.maxCastTime = maxCastTime;
        this.atbs = atbs;

        foreach (ATBCharacter atb in this.atbs) {
            maxIdleTime = Mathf.Max(atb.idleTime, maxIdleTime);
            maxCastTime = Mathf.Max(atb.castTime, maxCastTime);

            atb.nowTime = atb.idleTime;
        }
    }
    public void MyUpdate() {
        foreach (ATBCharacter atb in atbs) {
            atb.nowTime -= Time.deltaTime * atb.nowSpeed;
        }
    }
}
[System.Serializable]
public class ATBCharacter {
    public float idleTime;
    public float castTime;
    public float nowSpeed;
    public float nowTime;

    public ATBCharacter(float nowSpeed,float idleTime,float castTime) {
        this.nowSpeed = nowSpeed;
        this.idleTime = idleTime;
        this.castTime = castTime;
    }
}
