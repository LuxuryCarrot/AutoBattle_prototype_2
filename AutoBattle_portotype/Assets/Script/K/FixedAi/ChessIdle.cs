using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessIdle : ChessFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
    }

    private void Update()
    {
        manager.SetState(ChessStates.CHASE);
    }

    public override void EndState()
    {
        base.EndState();
    }
}
