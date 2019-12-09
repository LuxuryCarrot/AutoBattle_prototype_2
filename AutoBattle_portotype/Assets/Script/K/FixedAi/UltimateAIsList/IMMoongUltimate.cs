using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMMoongUltimate : UltimateAIParent
{
    private float temp;

    public override void Execute()
    {
        base.Execute();
        
        temp += Time.deltaTime;
        if (temp >= 2.0f)
        {
            temp = 0;
            manager.SetState(ChessStates.ATTACK);
        }
    }
    public override void uBulletInst()
    {
        base.uBulletInst();
        GameObject eff = Instantiate(Resources.Load("Prefabs/VFX/Shield/VFX_Ultimate_Shield"), manager.transform.position, Quaternion.identity) as GameObject;
        manager.protecting = true;
        manager.target.GetComponent<ChessFSMManager>().MeleeDamaged(manager.ultimateDamReal);
    }
}
