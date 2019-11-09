using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessUltimate : ChessFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
    }

    private void Update()
    {
        manager.ultiai.Execute();
    }

    public override void EndState()
    {
        base.EndState();
    }
}
