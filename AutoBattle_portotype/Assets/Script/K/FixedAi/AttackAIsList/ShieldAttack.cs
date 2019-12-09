using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : AttackAIParent
{
    public override void Execute()
    {
        base.Execute();
        if (manager.mana >= 100)
            manager.SetState(ChessStates.ULTIMATE);
    }

    public override void BulletInst()
    {
        base.BulletInst();
        GameObject eff = Instantiate(Resources.Load("Prefabs/VFX/Shield/VFX_Attack_Shield"), manager.target.transform.position, Quaternion.identity) as GameObject;
    }
}
