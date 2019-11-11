using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIParent : MonoBehaviour
{
    [HideInInspector]
    public ChessFSMManager manager;


    private void Awake()
    {
        manager = transform.GetComponentInParent<ChessFSMManager>();
    }
    public virtual void Execute() { }
}
