using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
    public float normalSpeed = 1.0f;
    
    public int TotalMemory;
    public Transform characterObjParent;
    public List<Character> characters;
    public List<GameObject> characterObjs;
	// Use this for initialization
	public void Initial () {
        characters = new List<Character>();

        for (int i = 0; i < this.transform.childCount; i++){
            Character character = this.transform.GetChild(i).GetComponent<Character>();
            Transform tempParent = characterObjParent.GetChild(i).transform;
            GameObject characterObj = (GameObject)Instantiate(character.characterPrefab, tempParent, false);
            character.characterObj = characterObj;
            characters.Add(character);
            characterObjs.Add(characterObj);
            for (int j = 0; j < TotalMemory; j++) character.process.Add(new Block()); 
        }

	}
    public void resetGame() {
        foreach (Character c in characters) {
            c.nowHP = c.maxHP;

            int idle = 0;
            int cast = 0;
            for (int i = 0; i < TotalMemory; i++){
                Block exist = c.process[i];
                if (exist.name != null){
                    cast = i + exist.GetCast();
                    idle += exist.GetCast();
                }
            }
            c.atb.Set(normalSpeed, idle, cast);
        }
    }

    // Update is called once per frame
    public void MyUpdate(){

        for (int i = 0; i < characters.Count; i++) {
            ATBCharacter atbC = characters[i].atb;
            if (!atbC.IsIdle) {
                int tempCast =  Mathf.FloorToInt(atbC.castTime - atbC.nowTime);
                if (tempCast != atbC.nowCast) {
                    if (characters[i].process[tempCast].name != null){
                        Debug.Log(i + "  " + characters[i].process[tempCast].name); 
                    }
                }
                atbC.nowCast = tempCast;
            }
        
        }

    }
}
