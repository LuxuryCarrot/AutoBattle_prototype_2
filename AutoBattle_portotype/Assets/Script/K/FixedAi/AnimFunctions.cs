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
    public void ManaCharge()
    {
        manager.ManaCharge();
    }
    public void PassiveStack()
    {
        manager.passive.Execute();
    }
    public void UltimateCheck()
    {
        manager.target.gameObject.GetComponent<ChessFSMManager>().MeleeDamaged(manager.ultimateDamReal);
    }
    public void BulletInst()
    {
        manager.attackai.BulletInst();
    }
    public void UltimateBulletInst()
    {
        manager.ultiai.uBulletInst();
    }
}
