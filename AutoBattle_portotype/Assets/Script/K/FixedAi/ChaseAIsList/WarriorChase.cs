using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChase : ChaseAIParent
{
    public override void Execute()
    {
        base.Execute();

        if (GameObject.FindGameObjectsWithTag("chess") != null && manager.target == null)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("chess");
            for (int i=0; i< objects.Length; i++)
            {
                if (objects[i] != manager.gameObject)
                    manager.target = objects[i].transform;
            }
            
        }

        if (manager.target==null)
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

        if (Vector3.SqrMagnitude(manager.transform.position - manager.target.position) < 1.0f)
            manager.SetState(ChessStates.ATTACK);

    }
}
