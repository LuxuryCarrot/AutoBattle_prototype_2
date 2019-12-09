using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBar : MonoBehaviour
{
    public GameObject target;
    public int level;

    public Image star1;
    public Image star2;
    public Image star3;

    private void Awake()
    {
        target = null;
        level = 0;
    }
    private void Update()
    {
        if (target == null)
        {
            if (level == 0)
                return;
            else
            {
                Destroy(gameObject);
            }
        }

        if (target.GetComponent<ChessFSMManager>().GetState() != ChessStates.IDLE)
            Destroy(gameObject);

        

        transform.position = target.transform.position + new Vector3(0, 1.5f, 0);

        level = target.GetComponent<ChessInfo>().iChessEvolutionRate;
        if(level==1)
        {
            star1.enabled = true;
            star2.enabled = false;
            star3.enabled = false;
        }
        else if(level==2)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = false;
        }
        else
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;
        }
    }
}
