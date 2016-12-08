using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
    public int TotalMemory;
    public Transform characterUIObjParent;
    public List<Character> characters;
    public List<GameObject> characterUIObjs;
	// Use this for initialization
	public void Initial () {
        characters = new List<Character>();

        for (int i = 0; i < this.transform.childCount; i++){
            Character character = this.transform.GetChild(i).GetComponent<Character>();
            GameObject UIObj = characterUIObjParent.GetChild(i).gameObject;
            character.characterUIObj = UIObj;
            characters.Add(character);
            characterUIObjs.Add(UIObj);

            UIObj.GetComponent<Image>().sprite = character.characterUI;
        }
	}

    // Update is called once per frame
    public void MyUpdate()
    {

    }
}
