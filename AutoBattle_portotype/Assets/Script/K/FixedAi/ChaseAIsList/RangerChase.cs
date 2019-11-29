using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerChase : ChaseAIParent
{
    public override void Execute()
    {
        base.Execute();

        if (GameObject.FindGameObjectsWithTag("chess") != null && manager.target == null)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("chess");
            GameObject final = null;
            for(int i=0; i<objects.Length; i++)
            {
                if(objects[i] != manager.gameObject 
                    && objects[i].GetComponent<ChessFSMManager>().ID != manager.ID)
                {
                    if (final == null)
                        final = objects[i];

                    else if(final.GetComponentInChildren<StatusLists>().HP >
                            objects[i].GetComponentInChildren<StatusLists>().HP)
                    {
                        final = objects[i];
                    }
                }
            }

            manager.target = final.transform;
        }

        if (manager.target == null)
            return;

        manager.transform.position = Vector3.MoveTowards(
                                      manager.transform.position,
                                      manager.target.position,
                                      manager.chaseSpeed * Time.deltaTime);



        Vector3 dir = manager.target.position - manager.transform.position;

        dir.y = 0;

        manager.transform.rotation = Quaternion.RotateTowards(manager.transform.rotation,
                                        Quaternion.LookRotation(dir), 540);

        if (Vector3.SqrMagnitude(manager.transform.position - manager.target.position) < manager.transform.GetComponentInChildren<StatusLists>().range)
            manager.SetState(ChessStates.ATTACK);
    }
}
