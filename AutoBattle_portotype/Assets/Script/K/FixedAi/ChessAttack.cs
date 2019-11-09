using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAttack : ChessFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
    }

    private void Update()
    {
        manager.attackai.Execute();
    }

    public override void EndState()
    {
        base.EndState();
    }
}
