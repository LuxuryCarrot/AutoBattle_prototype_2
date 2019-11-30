using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerChase : ChaseAIParent
{
    private bool isNear = false;
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

        if (!isNear)
        {
            manager.transform.position = Vector3.MoveTowards(
                                          manager.transform.position,
                                          manager.target.position,
                                          manager.chaseSpeed * Time.deltaTime);
        }
        else
            GotoBlock();

        Vector3 dir = manager.target.position - manager.transform.position;

        dir.y = 0;

        manager.transform.rotation = Quaternion.RotateTowards(manager.transform.rotation,
                                        Quaternion.LookRotation(dir), 540);

        if (Vector3.SqrMagnitude(manager.transform.position - manager.target.position) < manager.transform.GetComponentInChildren<StatusLists>().range)
            isNear = false;
    }
    private void GotoBlock()
    {
        Vector3 desti = 
            new Vector3(Mathf.Round(transform.position.x), transform.position.y,Mathf.Round(transform.position.z));

        manager.transform.position = Vector3.MoveTowards(
                                      manager.transform.position,
                                      desti,
                                      manager.chaseSpeed * Time.deltaTime);

        

        if (Vector3.SqrMagnitude(manager.transform.position - desti) < 0.1f)
            manager.SetState(ChessStates.ATTACK);
    }
}
