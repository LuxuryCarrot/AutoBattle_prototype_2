using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ChessStates
{
    IDLE=0,
    CHASE,
    ATTACK,
    ULTIMATE,
    JUMP,
    RUN
}

public class ChessFSMManager : MonoBehaviour
{
    //프라이벗 정보들. 
    private Animator anim;
    private ChessStates current;
    Dictionary<ChessStates, ChessFSMParent> FSMLists = new Dictionary<ChessStates, ChessFSMParent>();

    //기획자가 수정 가능한 필드
    public AttackAIParent attackai; 
    public ChaseAIParent chaseai;
    public UltimateAIParent ultiai;
    public bool isRun;
    public float hp;
    public float chaseSpeed;
    public float runSpeed;

    //퍼블릭 정보들.
    public Transform target;

    private void Awake()
    { 
        anim = GetComponent<Animator>();

        FSMLists.Add(ChessStates.IDLE, GetComponent<ChessIdle>());
        FSMLists.Add(ChessStates.CHASE, GetComponent<ChessChase>());
        FSMLists.Add(ChessStates.ATTACK, GetComponent<ChessAttack>());
        FSMLists.Add(ChessStates.ULTIMATE, GetComponent<ChessUltimate>());
        FSMLists.Add(ChessStates.JUMP, GetComponent<ChessJump>());
        FSMLists.Add(ChessStates.RUN, GetComponent<ChessRun>());

        current = ChessStates.IDLE;
        SetState(current);
    }

    public void SetState(ChessStates s)
    {
        if(s!=current)
          FSMLists[current].EndState();

        foreach(ChessFSMParent fsm in FSMLists.Values)
        {
            fsm.enabled = false;
        }
        current = s;

        FSMLists[current].enabled = true;
        FSMLists[current].BeginState();
    }

    public void MeleeDamaged()
    {
        if (isRun)
            SetState(ChessStates.RUN);
    }

    public void RangeDamaged()
    {

    }
}
