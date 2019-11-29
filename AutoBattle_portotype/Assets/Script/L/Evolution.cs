using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour
{
    public static Evolution instance;

    private int iSameHeroCount = 0;

    private string sHeroName;

    private void Awake()
    {
        Evolution.instance = this;
    }

    public void EvolutionCheck()
    {
        //GameObject[] chesss = GameObject.FindGameObjectsWithTag("chess");

        //for (int i = 0; i < chesss.Length; i++)
        //{
        //    Debug.Log(chesss[i].GetComponent<ChessInfo>().sMyName);
        //}

        //for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
        //{
        //    if (PlayerManager.instance.sInventory[i] != null)
        //    {
        //        sHeroName = PlayerManager.instance.sInventory[i];
        //        for (int j = 0; j < PlayerManager.instance.MaxHeroNumber; j++)
        //        {
        //            if (sHeroName == PlayerManager.instance.sInventory[j])
        //            {
        //                ++iSameHeroCount;
        //            }
        //        }
        //        if (GameManager.instance.bisRoundStarted == false)
        //        {
        //            Debug.Log("!!");
        //            for (int k = 0; k < PlayerManager.instance.iLevel; k++)
        //            {
        //                if (sHeroName == PlayerManager.instance.sGameBord[k])
        //                {
        //                    ++iSameHeroCount;
        //                }
        //            }
        //        }

        //        if (iSameHeroCount >= 3)
        //        {
        //            for (int a = 0; a < 3; a++)
        //            {

        //            }
        //        }
        //    }
        //    Debug.Log(sHeroName + " count : " + iSameHeroCount);
        //    iSameHeroCount = 0;
        //    sHeroName = null;
        //}
    }
}

