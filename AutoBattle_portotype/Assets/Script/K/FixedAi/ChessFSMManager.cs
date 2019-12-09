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
    public SynergyParent[] synergys;
    [HideInInspector]
    public PassiveAiParent passive;

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
    [HideInInspector]
    public float range;
    [HideInInspector]
    public float def;
    [HideInInspector]
    public string className;
    [HideInInspector]
    public float manaGet;
    [HideInInspector]
    public float mana;
    [HideInInspector]
    public float ultimateDam;

    [HideInInspector]
    public float chaseSpeedReal;
    [HideInInspector]
    public float runSpeedReal;
    [HideInInspector]
    public float damageReal;
    [HideInInspector]
    public float rangeReal;
    [HideInInspector]
    public float defReal;
    [HideInInspector]
    public float manaGetReal;
    [HideInInspector]
    public float ultimateDamReal;

    public bool protecting;
    public float protectTemp=1;
   

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
        synergys = transform.GetChild(0).GetComponents<SynergyParent>();
        passive = transform.GetChild(0).GetComponent<PassiveAiParent>();

        isRun = transform.GetChild(0).GetComponent<StatusLists>().isRun;
        hp = transform.GetChild(0).GetComponent<StatusLists>().HP;
        chaseSpeed = transform.GetChild(0).GetComponent<StatusLists>().speed;
        runSpeed = transform.GetChild(0).GetComponent<StatusLists>().runSpeed;
        damage= transform.GetChild(0).GetComponent<StatusLists>().damage;
        range = transform.GetChild(0).GetComponent<StatusLists>().range;
        def = transform.GetChild(0).GetComponent<StatusLists>().def;
        manaGet = transform.GetChild(0).GetComponent<StatusLists>().manaGet;
        ultimateDam = transform.GetChild(0).GetComponent<StatusLists>().ultimateDam;

        chaseSpeedReal = chaseSpeed;
        runSpeedReal = runSpeed;
        damageReal = damage;
        rangeReal = range;
        defReal = def;
        manaGetReal = manaGet;
        ultimateDamReal = ultimateDam;

        className = transform.GetChild(0).GetComponent<StatusLists>().className;

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void SetDefaultStat()
    {
        if(gameObject.GetComponent<ChessInfo>().iChessEvolutionRate==1)
        {
            isRun = transform.GetChild(0).GetComponent<StatusLists>().isRun;
            hp = transform.GetChild(0).GetComponent<StatusLists>().HP;
            chaseSpeed = transform.GetChild(0).GetComponent<StatusLists>().speed;
            runSpeed = transform.GetChild(0).GetComponent<StatusLists>().runSpeed;
            damage = transform.GetChild(0).GetComponent<StatusLists>().damage;
            range = transform.GetChild(0).GetComponent<StatusLists>().range;
            def = transform.GetChild(0).GetComponent<StatusLists>().def;
            manaGet = transform.GetChild(0).GetComponent<StatusLists>().manaGet;
            ultimateDam = transform.GetChild(0).GetComponent<StatusLists>().ultimateDam;
        }
        else if(gameObject.GetComponent<ChessInfo>().iChessEvolutionRate == 2)
        {
            isRun = transform.GetChild(0).GetComponent<StatusLists2>().isRun;
            hp = transform.GetChild(0).GetComponent<StatusLists2>().HP;
            chaseSpeed = transform.GetChild(0).GetComponent<StatusLists2>().speed;
            runSpeed = transform.GetChild(0).GetComponent<StatusLists2>().runSpeed;
            damage = transform.GetChild(0).GetComponent<StatusLists2>().damage;
            range = transform.GetChild(0).GetComponent<StatusLists2>().range;
            def = transform.GetChild(0).GetComponent<StatusLists2>().def;
            manaGet = transform.GetChild(0).GetComponent<StatusLists2>().manaGet;
            ultimateDam = transform.GetChild(0).GetComponent<StatusLists2>().ultimateDam;
        }
        chaseSpeedReal = chaseSpeed;
        runSpeedReal = runSpeed;
        damageReal = damage;
        rangeReal = range;
        defReal = def;
        manaGetReal = manaGet;
        ultimateDamReal = ultimateDam;

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
        if (protecting)
            return;

        GameObject eff = Instantiate(Resources.Load("Prefabs/VFX/VFX_Damage"), transform.position, Quaternion.identity) as GameObject;

        hp -= dam-defReal;
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

        if(protectTemp>0)
        {
            protectTemp -= Time.deltaTime;
            if(protectTemp<=0)
            {
                protecting = false;
                protectTemp = 1;
            }
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
        foreach(SynergyParent syn in synergys)
        {
            syn.IncreaseAbility();
        }
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

    public void ManaCharge()
    {
        mana += manaGetReal;
    }
    public ChessStates GetState()
    {
        return current;
    }
}
