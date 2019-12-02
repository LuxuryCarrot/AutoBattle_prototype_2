﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessIdle : ChessFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
        manager.hp = transform.GetChild(0).GetComponent<StatusLists>().HP;
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
