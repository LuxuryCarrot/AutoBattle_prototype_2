using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessChase : ChessFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
    }

    private void Update()
    {
        manager.chaseai.Execute();
    }


    public override void EndState()
    {
        base.EndState();
    }
}
