using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyParent : MonoBehaviour
{
    public ChessFSMManager manager;

    private void Awake()
    {
        manager = transform.parent.GetComponent<ChessFSMManager>();
    }

    virtual public void IncreaseAbility() { }
}
