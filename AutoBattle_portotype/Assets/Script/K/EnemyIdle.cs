using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("chess") != null)
        {
            manager.target = GameObject.FindGameObjectWithTag("chess").transform;
            manager.SetState(states.DASH);
        }
    }

    public override void EndState()
    {
        if (manager.target == null)
            return;
        Vector3 look = manager.target.position - transform.position;
        look.y = 0;
        transform.rotation = Quaternion.LookRotation(look);
        base.EndState();
    }
}
