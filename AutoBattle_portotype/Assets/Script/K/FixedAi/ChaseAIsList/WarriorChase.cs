using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChase : ChaseAIParent
{
    public override void Execute()
    {
        base.Execute();

        if (GameObject.FindGameObjectWithTag("chess") != null && manager.target == null)
        {
            manager.target = GameObject.FindGameObjectWithTag("chess").transform;
            
        }

        if (manager.target == null)
            return;

        if (Vector3.SqrMagnitude(manager.transform.position - manager.target.position) > 9.0f)
            manager.SetState(ChessStates.JUMP);

        manager.transform.position = Vector3.MoveTowards(
                                      manager.transform.position,
                                      manager.target.position,
                                      manager.chaseSpeed * Time.deltaTime);

        Vector3 dir = manager.target.position - manager.transform.position;

        dir.y = 0;

        manager.transform.rotation = Quaternion.RotateTowards(manager.transform.rotation,
                                        Quaternion.LookRotation(dir), 540);

    }
}
