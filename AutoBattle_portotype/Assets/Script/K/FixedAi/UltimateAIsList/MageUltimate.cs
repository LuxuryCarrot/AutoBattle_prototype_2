using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageUltimate : UltimateAIParent
{
    private float temp = 0;
    public override void Execute()
    {
        base.Execute();
        temp += Time.deltaTime;
        if (temp >= 2.0f)
            manager.SetState(ChessStates.ATTACK);
    }
    public override void uBulletInst()
    {
        base.uBulletInst();
        foreach(Vector3 pos in manager.passive.pos)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("chess");
            Instantiate(Resources.Load("Prefabs/VFX/VFX_Damage"), pos, Quaternion.identity);
            foreach (GameObject obj in targets)
            {

                
                if (obj.GetComponent<ChessFSMManager>().ID!=manager.ID && Vector3.SqrMagnitude(obj.transform.position-pos)<= 3.0f)
                {
                    obj.GetComponent<ChessFSMManager>().MeleeDamaged(manager.ultimateDamReal);
                }
            }
        }
    }
}
