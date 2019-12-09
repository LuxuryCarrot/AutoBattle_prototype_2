using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorUltimate : UltimateAIParent
{
    private float temp;
    
    public override void Execute()
    {
        base.Execute();
        temp += Time.deltaTime;
        if(temp>=3.2f)
        {
            temp = 0;
            manager.SetState(ChessStates.ATTACK);
        }
    }
    public override void uBulletInst()
    {
        base.uBulletInst();
        GameObject eff = Instantiate(Resources.Load("Prefabs/VFX/Sword/VFX_Ultimate_Sword"), manager.transform.position, Quaternion.identity) as GameObject;
    }
}
