using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAttack : ChessFSMParent
{
    public override void BeginState()
    {
        base.BeginState();
        manager.anim.SetInteger("Param", (int)ChessStates.ATTACK);
        manager.isTargeted = false;
        Vector3 look = manager.target.position - transform.position;
        look.y = 0;
        transform.rotation = Quaternion.LookRotation(look);
    }

    private void Update()
    {
        manager.attackai.Execute();
        if(manager.target.gameObject.GetComponent<ChessFSMManager>().hp<=0)
        {
            manager.target = null;
            manager.SetState(ChessStates.CHASE);
            
        }

        if (manager.target == null || Vector3.SqrMagnitude(manager.target.position - transform.position) > Mathf.Pow(manager.range, 2)) 
        {
            manager.SetState(ChessStates.CHASE);
        }
    }

    public override void EndState()
    {
        base.EndState();
    }
}
