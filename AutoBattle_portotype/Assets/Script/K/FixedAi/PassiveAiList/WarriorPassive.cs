using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPassive : PassiveAiParent
{
    public override void Execute()
    {
        base.Execute();
        manager.ultimateDamReal += 10;
    }
}
