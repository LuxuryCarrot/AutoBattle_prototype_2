using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttack : AttackAIParent
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
        GameObject eff = Instantiate(Resources.Load("Prefabs/VFX/Sword/VFX_Attack_Sword"), manager.transform.position, manager.transform.rotation) as GameObject;
    }
}
