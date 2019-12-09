using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUltimate : UltimateAIParent
{
    public override void BulletInst()
    {
        base.Execute();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("chess");
        int i = 2;
        foreach (GameObject obj in targets)
        {
            if (Vector3.SqrMagnitude(obj.transform.position - manager.target.position) <= 3.0f)
            {
                obj.GetComponent<ChessFSMManager>().MeleeDamaged(manager.ultimateDamReal);
                i--;
            }
            if (i <= 0)
                break;
        }
    }

    public override void Execute()
    {
        base.Execute();
        manager.mana -= 20 * Time.deltaTime;

        if(manager.mana<=0)
        {
            manager.mana = 0;
            manager.SetState(ChessStates.ATTACK);
        }
    }
}
