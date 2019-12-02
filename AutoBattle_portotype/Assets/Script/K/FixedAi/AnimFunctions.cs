using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFunctions : MonoBehaviour
{
    ChessFSMManager manager;
    private void Awake()
    {
        manager = transform.parent.gameObject.GetComponent<ChessFSMManager>();
    }
    public void AttackCheck()
    {
        manager.target.gameObject.GetComponent<ChessFSMManager>().MeleeDamaged(manager.damageReal);
    }
}
