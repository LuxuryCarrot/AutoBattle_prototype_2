using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMPBarScripts : MonoBehaviour
{
    public float hp;
    public float mp;
    public GameObject target;

    public Slider hpbar;
    public Slider mpbar;

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

        hpbar.value = hp / 100;
        mpbar.value = mp / 100;
    }

}
