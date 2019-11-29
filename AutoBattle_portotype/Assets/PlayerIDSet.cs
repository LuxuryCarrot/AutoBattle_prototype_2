using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDSet : MonoBehaviour
{
    public static int playerID;
    public static int AIID;

    private void Awake()
    {
        playerID = Random.Range(1, 100);
        for(;playerID==AIID;)
        {
            AIID = Random.Range(1, 100);
        }
    }
}
