using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAIParent : MonoBehaviour
{
    public ChessFSMManager manager;


    private void Awake()
    {
        manager = GetComponent<ChessFSMManager>();
    }

    public virtual void Execute() { }
}
