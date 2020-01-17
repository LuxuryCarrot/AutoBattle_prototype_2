using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerUltimate : UltimateAIParent
{
    private float temp;

    public override void Execute()
    {
        base.Execute();
        temp += Time.deltaTime;
        if (temp >= 2.0f)
        {
            temp = 0;
            manager.SetState(ChessStates.ATTACK);
        }
    }
    public override void uBulletInst()
    {
        base.uBulletInst();
        GameObject[] targets = GameObject.FindGameObjectsWithTag("chess");
        Vector3 tarVec=Vector3.zero;

        foreach(GameObject obj in targets)
        {
            if(obj.GetComponent<ChessFSMManager>().ID != manager.ID)
            {
                tarVec = obj.transform.position;
                break;
            }
        }

        GameObject inst = Instantiate(Resources.Load("Prefabs/VFX/InstanceExploseEff"), tarVec, Quaternion.identity) as GameObject;

        inst.GetComponent<ManaField>().id = manager.ID;
        inst.GetComponent<ManaField>().level = manager.level -1;
    }
}
