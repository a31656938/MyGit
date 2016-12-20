﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
    public float normalSpeed = 1.0f;
    
    public int TotalMemory;
    public Transform characterObjParent;
    public List<Character> characters;
    public List<GameObject> characterObjs;

    public States player;
    public States monster;
    public float buffTimer;
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
        player.nowHP = player.maxHP;
        monster.nowHP = monster.maxHP;
        buffTimer = 0;
        foreach (Character c in characters) {
            int idle = 0;
            int cast = 0;
            for (int i = 0; i < c.process.Count; i++){
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
                        UseSkill(i, characters[i].process[tempCast]);
                    }
                }
                atbC.nowCast = tempCast;
            }
        }
        
        // buff 減少
        buffTimer += Time.deltaTime;
        if (buffTimer >= 1.0f) {
            for (int i = player.buffs.Count - 1; i >= 0; i--) {
                UseBuff(0,player.buffs[i]);
                player.buffs[i].time -= 1;
                if (player.buffs[i].time == 0) {
                    player.buffs.RemoveAt(i);
                }
            }
            for (int i = monster.buffs.Count - 1; i >= 0; i--){
                UseBuff(1, monster.buffs[i]);
                monster.buffs[i].time -= 1;
                if (monster.buffs[i].time == 0){
                    monster.buffs.RemoveAt(i);
                }
            }

            buffTimer = 0;
        }


        if (player.nowHP <= 0) { 
            ///game over
        }
        else if (monster.nowHP <= 0) {
            ///game over
        }
    }
    void UseBuff(int characterIndex, Buff buff){
        if (characterIndex == 0){
            Debug.Log(buff.name);
        }
        else {
            Debug.Log(buff.name);
        }
    
    }
    void UseSkill(int characterIndex, Block block) {
        if (characterIndex < 3){
            if (block.GetType() == typeof(AttackBlock)) {
                monster.nowHP -= ((AttackBlock)block).attack;
                characterObjs[3].GetComponent<Animator>().SetTrigger("hit");
            }
            else if (block.GetType() == typeof(BuffBlock)) { }
            else if (block.GetType() == typeof(IFBlock)) { }
            else if (block.GetType() == typeof(MultipleBlock)) { }

            if (block.buff != null) monster.buffs.Add(block.buff);
        }
        else {
            if (block.GetType() == typeof(AttackBlock))
            {
                player.nowHP -= ((AttackBlock)block).attack;

                for (int i = 0; i < 3; i++) {
                    characterObjs[i].GetComponent<Animator>().SetTrigger("hit");
                }

            }
            else if (block.GetType() == typeof(BuffBlock)) { }
            else if (block.GetType() == typeof(IFBlock)) { }
            else if (block.GetType() == typeof(MultipleBlock)) { }

            GameManager.Inst.uiManager.UpdateStates();
        }
    }
}
