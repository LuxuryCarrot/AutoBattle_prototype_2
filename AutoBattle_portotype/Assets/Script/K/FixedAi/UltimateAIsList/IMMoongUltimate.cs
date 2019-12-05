using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMMoongUltimate : UltimateAIParent
{
    private float temp;

    public override void Execute()
    {
        base.Execute();
        temp += Time.deltaTime;
        if (temp >= 2.0f)
        {
            temp = 0;
            manager.SetState(ChessStates.ATTACK);
        }
    }
}
