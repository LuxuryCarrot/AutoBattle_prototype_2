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
                if (objects[i] != manager.gameObject 
                    && objects[i].GetComponent<ChessFSMManager>().ID != manager.ID 
                    && !manager.isTargeted
                    && objects[i].GetComponent<ChessFSMManager>().hp>0)
                {
                    manager.target = objects[i].transform;
                    manager.target.gameObject.GetComponent<ChessFSMManager>().JumpTargeted(manager);
                    manager.anim.SetBool("miss", false);
                }
            }
        }

        if (manager.target == null)
        {
            manager.anim.SetBool("miss", true);
            return;
        }

        Vector3 targetVec = manager.target.position;

        if ((manager.transform.position - manager.target.position).x >= 0)
            targetVec += new Vector3(2, 0, 0);
        else
            targetVec += new Vector3(-2, 0, 0);

        if ((manager.transform.position - manager.target.position).z >= 0)
            targetVec += new Vector3(0, 0, 2);
        else
            targetVec += new Vector3(0, 0, -2);

        if (!manager.isTargeted)
        {
            if (Vector3.SqrMagnitude(manager.transform.position - targetVec) > 36.0f)
                manager.SetState(ChessStates.JUMP);

            manager.transform.position = Vector3.MoveTowards(
                                          manager.transform.position,
                                          targetVec,
                                          manager.chaseSpeed * Time.deltaTime);

        }

        Vector3 dir = targetVec - manager.transform.position;

        dir.y = 0;

        manager.transform.rotation = Quaternion.RotateTowards(manager.transform.rotation,
                                        Quaternion.LookRotation(dir), 1080);

        if (Vector3.SqrMagnitude(manager.transform.position - targetVec) < 8.0f)
            manager.SetState(ChessStates.ATTACK);

    }
}
