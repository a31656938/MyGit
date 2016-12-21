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

    public List<States> states;
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
        states[0].nowHP = states[0].maxHP;
        states[1].nowHP = states[1].maxHP;
        states[0].buffs.Clear();
        states[1].buffs.Clear();
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
        for (int j = 0; j <= 1; j++) {
            for (int i = states[j].buffs.Count - 1; i >= 0; i--){
                states[j].buffs[i].timer += Time.deltaTime;
                if (states[j].buffs[i].timer >= 1){
                    UseBuff(j, states[j].buffs[i]);
                    states[j].buffs[i].count--;
                    states[j].buffs[i].timer = 0;
                    if (states[j].buffs[i].count <= 0) {
                        states[j].buffs.RemoveAt(i);
                    }
                }
            }
        }
        


        GameManager.Inst.uiManager.UpdateStates();

        if (states[0].nowHP <= 0)
        { 
            ///game over
        }
        else if (states[1].nowHP <= 0)
        {
            ///game over
        }
    }
    void UseBuff(int characterIndex, Buff buff){
        if (buff.name == "fired1"){
            states[characterIndex].nowHP -= 50;
            if (characterIndex == 0){
                for (int i = 0; i < 3; i++) {
                    GameManager.Inst.uiManager.HitEffect(i, "Fire1");
                }
            }
            else GameManager.Inst.uiManager.HitEffect(3, "Fire1"); ;
        }
        else if (buff.name == "fired2") {
            states[characterIndex].nowHP -= 100;
            if (characterIndex == 0){
                for (int i = 0; i < 3; i++) {
                    GameManager.Inst.uiManager.HitEffect(i, "Fire2");
                }
            }
            else GameManager.Inst.uiManager.HitEffect(3, "Fire2"); ;
        }
    
    }
    void UseSkill(int characterIndex, Block block) {
        int target = 1;
        if (characterIndex == 3) target = 0;

        if (block.GetType() == typeof(AttackBlock)){
            states[target].nowHP -= ((AttackBlock)block).attack; 
            if (block.buff != null) states[target].buffs.Insert(0,block.buff);
        }
        else if (block.GetType() == typeof(BuffBlock)) { }
        else if (block.GetType() == typeof(IFBlock)) { }
        else if (block.GetType() == typeof(MultipleBlock)) { }

        // call ui hit effect
        if (target == 1){
            GameManager.Inst.uiManager.HitEffect(3, block.effectName); ;
        }
        else {
            for (int i = 0; i < 3; i++) {
                GameManager.Inst.uiManager.HitEffect(i, block.effectName); ;
            }
        }

        // buff 加成
        if (block.name == "Oil")
        {
            Buff oiled = states[target].HaveBuff("oiled");
            Buff fired1 = states[target].HaveBuff("fired1");
            Buff fired2 = states[target].HaveBuff("fired2");
            if (fired2 != null){
                states[target].buffs.Remove(oiled);
                fired2.count += 3;
            }
            else if (oiled != null && fired1 != null){
                states[target].buffs.Remove(oiled);
                states[target].buffs.Remove(fired1);
                states[target].buffs.Add(new Buff("fired2", "大燃燒吧", 3));
            }
            else if (oiled != null) {
                if (states[target].CountBuff("oiled") > 1) states[target].buffs.Remove(oiled);
                oiled.count += 3;
            }
        }
        else if (block.name == "FireBall")
        {
            Buff oiled = states[target].HaveBuff("oiled");
            Buff fired1 = states[target].HaveBuff("fired1");
            Buff fired2 = states[target].HaveBuff("fired2");
            Buff wet = states[target].HaveBuff("wet");
            if (wet != null && fired1 != null) {
                states[target].buffs.Remove(fired1);
                states[target].buffs.Remove(wet);
            }
            else if (fired2 != null) {
                states[target].buffs.Remove(fired1);
                fired2.count += 3;
            }
            else if (oiled != null && fired1 != null){
                states[target].buffs.Remove(oiled);
                states[target].buffs.Remove(fired1);
                states[target].buffs.Add(new Buff("fired2", "大燃燒吧", 3));
            }
            else if (fired1 != null){
                if (states[target].CountBuff("fired1") > 1) states[target].buffs.Remove(fired1);
                fired1.count += 3;
            }
        }
        else if (block.name == "WindSword") {
            Buff fired2 = states[target].HaveBuff("fired2");
            if (fired2 != null) {
                states[target].buffs.Remove(fired2);
                
                UseSkill(characterIndex, new AttackBlock("explosion", "boom", 0, "Fire3", null, 300));              
            }
        }
        else if (block.name == "WaterGun"){
            Buff fired1 = states[target].HaveBuff("fired1");
            Buff fired2 = states[target].HaveBuff("fired2");
            Buff wet = states[target].HaveBuff("wet");
            if (fired2 != null) {
                states[target].buffs.Remove(fired2);
                states[target].buffs.Remove(wet);
                states[target].buffs.Add(new Buff("fired1", "燃燒吧", 3)); 
            }
            else if (fired1 != null) {
                states[target].buffs.Remove(fired1);
                states[target].buffs.Remove(wet);
            }
            else if (wet != null) {
                if (states[target].CountBuff("wet")>1) states[target].buffs.Remove(wet);
                wet.count += 3;
            }
        }
              
    }
}
