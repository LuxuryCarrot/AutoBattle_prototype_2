using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaField : MonoBehaviour
{
    public int id;
    public int level;

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("chess");

        foreach(GameObject obj in targets)
        {
            if(obj.GetComponent<ChessFSMManager>().ID!=id 
                && Vector3.SqrMagnitude(obj.transform.position-transform.position)<=36.0f)
            {
                obj.GetComponent<ChessFSMManager>().mana -= (5 + 2.5f * level) * Time.deltaTime;
            }
        }
    }
}
