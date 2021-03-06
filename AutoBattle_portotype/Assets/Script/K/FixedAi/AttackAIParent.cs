﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIParent : MonoBehaviour
{
    [HideInInspector]
    public ChessFSMManager manager;


    private void Awake()
    {
        manager = transform.GetComponentInParent<ChessFSMManager>();
    }

    public virtual void Execute() { }
    public virtual void BulletInst() { }
}
