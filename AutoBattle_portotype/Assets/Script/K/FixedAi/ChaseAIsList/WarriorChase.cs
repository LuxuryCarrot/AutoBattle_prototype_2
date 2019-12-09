using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorChase : ChaseAIParent
{
    public override void Execute()
    {
        base.Execute();

        manager.transform.position = new Vector3(manager.transform.position.x, 0.7f, manager.transform.position.z);

        if (GameObject.FindGameObjectsWithTag("chess") != null && manager.target == null)
        {
            isNear = false;
            GameObject[] objects = GameObject.FindGameObjectsWithTag("chess");
            for (int i=0; i< objects.Length; i++)
            {
                if (objects[i] != manager.gameObject 
                    && objects[i].GetComponent<ChessFSMManager>().ID != manager.ID 
                    && !manager.isTargeted
                    && objects[i].GetComponent<ChessFSMManager>().hp>0
                    && objects[i].GetComponent<ChessFSMManager>().GetState()!=ChessStates.JUMP)
                {
                    manager.target = objects[i].transform;
                    manager.target.gameObject.GetComponent<ChessFSMManager>().JumpTargeted(manager);
                    manager.anim.SetBool("miss", false);
                    break;
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
            targetVec += new Vector3(-2, 0, 0);
        else
            targetVec += new Vector3(2, 0, 0);

        if ((manager.transform.position - manager.target.position).z >= 0)
            targetVec += new Vector3(0, 0, -2);
        else
            targetVec += new Vector3(0, 0, 2);

        if (!manager.isTargeted)
        {
            if (Vector3.SqrMagnitude(manager.transform.position - targetVec) > 36.0f)
                manager.SetState(ChessStates.JUMP);

            if (!isNear)
            {
                manager.transform.position = Vector3.MoveTowards(
                                              manager.transform.position,
                                              manager.target.position,
                                              manager.chaseSpeedReal * Time.deltaTime);
                Debug.Log(targetVec);
            }
            else
            {
                GotoBlock();
            }

        }

        Vector3 dir = targetVec - manager.transform.position;

        dir.y = 0;

        manager.transform.rotation = Quaternion.RotateTowards(manager.transform.rotation,
                                        Quaternion.LookRotation(dir), 1080);

        if (Vector3.SqrMagnitude(manager.transform.position - manager.target.position) < 9.0f)

        {
            isNear = true;
            if (manager.isTargeted)
                manager.SetState(ChessStates.ATTACK);
        }

    }
}
