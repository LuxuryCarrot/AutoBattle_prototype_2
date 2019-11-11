using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessFSMParent : MonoBehaviour
{
    [HideInInspector]
    public ChessFSMManager manager;

    private void Awake()
    {
        manager = GetComponent<ChessFSMManager>();
    }

    public virtual void BeginState() { }
    public virtual void EndState() { }
}
