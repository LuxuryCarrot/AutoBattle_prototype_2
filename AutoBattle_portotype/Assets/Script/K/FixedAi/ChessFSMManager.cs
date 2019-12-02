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
    RUN,
    DIE
}

public class ChessFSMManager : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    private ChessStates current;
    private bool isSettled;
    private bool isEnqueued;
    public GameManager gameManager;
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
    [HideInInspector]
    public int ID;
    [HideInInspector]
    public bool isTargeted;
    [HideInInspector]
    public float damage;
    public float range;
    
   

    private void Awake()
    {
        target = null;
        isSettled = false;
        isEnqueued = false;
        isTargeted = false;
        
        anim = transform.GetChild(0).GetComponent<Animator>();

        FSMLists.Add(ChessStates.IDLE, GetComponent<ChessIdle>());
        FSMLists.Add(ChessStates.CHASE, GetComponent<ChessChase>());
        FSMLists.Add(ChessStates.ATTACK, GetComponent<ChessAttack>());
        FSMLists.Add(ChessStates.ULTIMATE, GetComponent<ChessUltimate>());
        FSMLists.Add(ChessStates.JUMP, GetComponent<ChessJump>());
        FSMLists.Add(ChessStates.RUN, GetComponent<ChessRun>());
        FSMLists.Add(ChessStates.DIE, GetComponent<ChessDie>());

        current = ChessStates.IDLE;
        SetState(current);

        attackai = transform.GetChild(0).GetComponent<AttackAIParent>();
        chaseai = transform.GetChild(0).GetComponent<ChaseAIParent>();
        ultiai =  transform.GetChild(0).GetComponent<UltimateAIParent>();

        isRun = transform.GetChild(0).GetComponent<StatusLists>().isRun;
        hp = transform.GetChild(0).GetComponent<StatusLists>().HP;
        chaseSpeed = transform.GetChild(0).GetComponent<StatusLists>().speed;
        runSpeed = transform.GetChild(0).GetComponent<StatusLists>().runSpeed;
        damage= transform.GetChild(0).GetComponent<StatusLists>().damage;
        range = transform.GetChild(0).GetComponent<StatusLists>().range;

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
        anim.SetBool("miss", false);
        anim.SetInteger("Param", (int)current);
        FSMLists[current].BeginState();
    }

    public void MeleeDamaged(float dam)
    {
        hp -= dam;
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

            if(transform.position.y<=0.7f)
            {
                isSettled = false;
                Instantiate(Resources.Load("Prefabs/VFX/VFX_Jump"), transform.position - new Vector3(0, 1, 0), Quaternion.identity);
                transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
            }
        }

        if(hp<=0)
        {
            
            SetState(ChessStates.DIE);
            anim.SetInteger("Param", (int)current);
        }
    }

    public void EnQueueThis()
    {
        if (!isEnqueued)
        {
            isEnqueued = true;

            gameManager.chessList.Add(this.gameObject);
            ID = PlayerIDSet.playerID;
        }
    }

    public void DeQueueThis()
    {
        //gameManager.nextRoundQueue.Enqueue(this.gameObject);
        isEnqueued = false;
        SetState(ChessStates.CHASE);
    }

    public void BenchIn()
    {
        if (isEnqueued == true)
        {
            gameManager.chessList.Remove(this.gameObject);
            if (gameObject.transform.parent != null)
                gameObject.transform.parent = null;
            Debug.Log("Bench");
        }

        isEnqueued = false;
    }

    public void JumpTargeted(ChessFSMManager by)
    {
        isTargeted = true;
        target = by.gameObject.transform;
        anim.SetBool("miss", true);
    }
}
