using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePassive : PassiveAiParent
{
    private Vector3 targetpos=Vector3.zero;

    public override void Execute()
    {
        base.Execute();

        if (targetpos != manager.target.position)
        {
            targetpos = manager.target.position;
            pos.Enqueue(targetpos);
        }
    }

    public override Vector3 GetPassiveData()
    {
        if (pos.Count != 0)
            return pos.Dequeue();
        else
            return Vector3.zero;
    }

}
