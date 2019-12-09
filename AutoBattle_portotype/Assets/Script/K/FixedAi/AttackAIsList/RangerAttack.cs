using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : AttackAIParent
{
    public override void Execute()
    {
        base.Execute();
        if (manager.mana >= 100)
            manager.SetState(ChessStates.ULTIMATE);

        if (manager.target.GetComponent<ChessFSMManager>().hp <= 0)
            manager.SetState(ChessStates.CHASE);
    }
}
