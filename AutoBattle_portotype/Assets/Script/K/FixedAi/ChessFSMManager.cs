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
    [HideInInspector]
    public Animator anim;
    private ChessStates current;
    private bool isSettled;
    Dictionary<ChessStates, ChessFSMParent> FSMLists = new Dictionary<ChessStates, ChessFSMParent>();

    [HideInInspector]
    public AttackAIParent attackai;
    [HideInInspector]
    public ChaseAIParent chaseai;
    [HideInInspector]
    public UltimateAIParent ultiai;

    [HideInInspector]
    public bool isRun;
    [HideInInspector]
    public float hp;
    [HideInInspector]
    public float chaseSpeed;
    [HideInInspector]
    public float runSpeed;
    [HideInInspector]
    public Transform target=null;
   

    private void Awake()
    {
        target = null;
        isSettled = false;
  
        anim = transform.GetChild(0).GetComponent<Animator>();

        FSMLists.Add(ChessStates.IDLE, GetComponent<ChessIdle>());
        FSMLists.Add(ChessStates.CHASE, GetComponent<ChessChase>());
        FSMLists.Add(ChessStates.ATTACK, GetComponent<ChessAttack>());
        FSMLists.Add(ChessStates.ULTIMATE, GetComponent<ChessUltimate>());
        FSMLists.Add(ChessStates.JUMP, GetComponent<ChessJump>());
        FSMLists.Add(ChessStates.RUN, GetComponent<ChessRun>());

        current = ChessStates.IDLE;
        SetState(current);

        attackai = transform.GetChild(0).GetComponent<AttackAIParent>();
        chaseai = transform.GetChild(0).GetComponent<ChaseAIParent>();
        ultiai =  transform.GetChild(0).GetComponent<UltimateAIParent>();

        isRun = transform.GetChild(0).GetComponent<StatusLists>().isRun;
        hp = transform.GetChild(0).GetComponent<StatusLists>().HP;
        chaseSpeed = transform.GetChild(0).GetComponent<StatusLists>().speed;
        runSpeed = transform.GetChild(0).GetComponent<StatusLists>().runSpeed;
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

    public void Settled()
    {
        isSettled = true;
        
    }

    private void Update()
    {
        if(isSettled)
        {
            transform.position -= new Vector3(0, 2 * Time.deltaTime, 0);

            if(transform.position.y<=2.0f)
            {
                isSettled = false;
              
                transform.position = new Vector3(transform.position.x, 2.0f, transform.position.z);
            }
        }
    }
}
