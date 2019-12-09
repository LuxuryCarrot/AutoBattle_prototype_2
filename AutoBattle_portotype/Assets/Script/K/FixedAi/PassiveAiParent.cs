using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAiParent : MonoBehaviour
{
    [HideInInspector]
    public ChessFSMManager manager;
    public Queue<Vector3> pos =new Queue<Vector3>();


    private void Awake()
    {
        manager = transform.GetComponentInParent<ChessFSMManager>();
    }
    public virtual void Execute() { }
    public virtual Vector3 GetPassiveData() { return Vector3.zero; }
}
