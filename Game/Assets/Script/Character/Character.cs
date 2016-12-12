using UnityEngine;
using System.Collections;
using System.Collections.Generic;
abstract public class Character : MonoBehaviour{
    public ATBCharacter atb;
    public Sprite atbIcon;
    public GameObject characterPrefab;
    public GameObject characterObj;
    public List<Block> process;
}
