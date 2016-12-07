﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuffBlock : Block
{
    public bool benefit;

    public BuffBlock(string name, string description, int idle, bool benefit)
    {
        BaseConstructor(name, description, idle);
        this.benefit = benefit;
    }
}
