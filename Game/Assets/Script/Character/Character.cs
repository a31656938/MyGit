using UnityEngine;
using System.Collections;
using System.Collections.Generic;
abstract public class Character : MonoBehaviour{
    public GameObject characterUIObj;
    public ATBCharacter atb;
    public Sprite atbIcon;
    public Sprite characterUI;
    public List<Block> process;
}
