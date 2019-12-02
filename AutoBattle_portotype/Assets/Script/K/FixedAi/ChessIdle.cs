using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessIdle : ChessFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
        manager.hp = transform.GetChild(0).GetComponent<StatusLists>().HP;
        manager.chaseSpeedReal = manager.chaseSpeed;
        manager.runSpeedReal = manager.runSpeed;
        manager.damage = manager.damageReal;
        manager.rangeReal = manager.range;
        manager.defReal = manager.def;
    }

    private void Update()
    {
        //manager.SetState(ChessStates.CHASE);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
