using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
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

            for (int j = 0; j < TotalMemory; j++) character.process.Add(null); 
        }

	}

    // Update is called once per frame
    public void MyUpdate()
    {

    }
}
