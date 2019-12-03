using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIParent : MonoBehaviour
{
    [HideInInspector]
    public ChessFSMManager manager;
    public Vector3 desti;
    public bool isNear;

    private void Awake()
    {
        manager = transform.GetComponentInParent<ChessFSMManager>();
        desti = Vector3.zero;
    }
    public virtual void Execute() { }

    public void GotoBlock()
    {
        if (desti == Vector3.zero)
        {
            desti =
                new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

            if (desti.x % 2 > 0)
                desti.x = Random.Range(0, 1) >= 0.5f ? desti.x + 1 : desti.x - 1;
            if (desti.z % 2 > 0)
                desti.z = Random.Range(0, 1) >= 0.5f ? desti.z + 1 : desti.z - 1;

        }
        manager.transform.position = Vector3.MoveTowards(
                                      manager.transform.position,
                                      desti,
                                      manager.chaseSpeedReal * Time.deltaTime);



        if (Vector3.SqrMagnitude(manager.transform.position - desti) < 0.04f)
        {
            manager.transform.position = desti;
            desti = Vector3.zero;
           
            manager.SetState(ChessStates.ATTACK);
        }
    }
}
