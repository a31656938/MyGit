﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Block {
    public string name;
    public string description;
    public Sprite icon;
    public Color color;
    public Buff buff;
    protected int cast;

    public Block() { }
    public void SetBase(string name, string description, int cast,Color color,Buff buff)
    {
        this.name = name;
        this.description = description;
        this.cast = cast;
        icon = Resources.Load<Sprite>("BlockIcon/" + name);
        this.color = color;
        this.buff = buff;
    }
    public string GetDescription() {
        return description;
    }
    virtual public int GetCast(){
        return this.cast;
    }
}
