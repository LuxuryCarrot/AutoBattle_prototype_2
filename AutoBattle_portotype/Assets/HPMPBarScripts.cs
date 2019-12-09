using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMPBarScripts : MonoBehaviour
{
    public float hp;
    public float mp;
    public GameObject target;
    public int level;

    public Slider hpbar;
    public Slider mpbar;

    public Image star1;
    public Image star2;
    public Image star3;

    private void Awake()
    {
        target = null;
        hp = 0;
        mp = 0;
    }
    private void Update()
    {
        
        if (target == null)
            return;

        transform.position = target.transform.position + new Vector3(0, 1.5f, 0);
        if (target.GetComponent<ChessFSMManager>().GetState() == ChessStates.IDLE)
            Destroy(gameObject);

        hp = target.GetComponent<ChessFSMManager>().hp;
        mp = target.GetComponent<ChessFSMManager>().mana;

        int level = target.GetComponent<ChessInfo>().iChessEvolutionRate;
        if(level==1)
         hpbar.value = hp / target.transform.GetComponentInChildren<StatusLists>().HP;
        else
            hpbar.value = hp / target.transform.GetComponentInChildren<StatusLists2>().HP;
        mpbar.value = mp / 100;
    }

}
