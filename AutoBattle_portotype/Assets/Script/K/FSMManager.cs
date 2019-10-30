using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum states
{
    IDLE=0,
    DASH,
    BATTLE
}
public class FSMManager : MonoBehaviour
{
    Dictionary<states, EnemyFSMParent> stateLists = new Dictionary<states, EnemyFSMParent>();
    private states current;
    public Transform target;

    private void Awake()
    {
        stateLists.Add(states.IDLE, GetComponent<EnemyIdle>());
        stateLists.Add(states.DASH, GetComponent<EnemyDash>());
        stateLists.Add(states.BATTLE, GetComponent<EnemyBattle>());
        current = states.IDLE;
        SetState(current);
    }
    public void SetState(states num)
    {
        stateLists[current].EndState();
        foreach(EnemyFSMParent fsm in stateLists.Values)
        {
            fsm.enabled = false;
        }
        current = num;

        stateLists[num].enabled = true;
        stateLists[num].BeginState();
    }
}
