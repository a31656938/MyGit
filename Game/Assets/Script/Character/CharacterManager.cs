using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
    public int TotalMemory;
    public List<Character> characters;

	// Use this for initialization
	public void Initial () {
        characters = new List<Character>();

        for (int i = 0; i < this.transform.childCount; i++){
            Character temp = this.transform.GetChild(i).GetComponent<Character>();
            characters.Add(temp);
        }
	}

    // Update is called once per frame
    public void MyUpdate()
    {

    }
}
