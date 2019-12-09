using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSynergy : SynergyParent
{
    public override void IncreaseAbility()
    {
        base.IncreaseAbility();
        int count = 0;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("chess");
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].GetComponent<ChessFSMManager>().className == manager.className
                && objs[i].GetComponent<ChessFSMManager>().ID == PlayerIDSet.playerID)
                count++;
        }

        if (count >= 2)
        {
            manager.ultimateDamReal += 140;
            SynergyCanvas.instance.MageSetSynergy(2);
        }


    }
}
