using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Character : MonoBehaviour{
    public int maxHP;
    public int nowHP;
    public int nowEngery;


    public ATBCharacter atb;
    public Sprite atbIcon;
    public GameObject characterPrefab;
    public GameObject characterObj;
    public List<Block> process;
    public Color color;
}
