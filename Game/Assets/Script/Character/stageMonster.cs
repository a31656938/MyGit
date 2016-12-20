using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class stageMonster  {

    public static List<Block> getProcess(int level) {
        List<Block> re = new List<Block>();

        if (level == 0){
            re.Add(new AttackBlock("block00", "", 2, null, 20));
            re.Add(new Block());

        }
        else { 
            //其他
        }

        return re;
    }
}
