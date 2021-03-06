﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessInitialize : MonoBehaviour
{
    private string[] chessNames = { "ParkWarrior", "ParkShield", "Mage", "Archer", "Ranger" };
    public GameObject Store;
    public GameObject tiles1;
    public GameObject tiles2;
    public GameObject banner;

    public Sprite[] images;

    private Transform[] slots = new Transform[5];

    private void Awake()
    {
        for(int i=0; i<5; i++)
        {
            slots[i] = banner.transform.GetChild(i);
        }
        ReRoll();
    }

    public void InitializingInst()
    {
        Transform target=null;
        for(int i=0; i<8; i++)
        {
            if (Store.transform.GetChild(i).childCount == 0)
            {
                target = Store.transform.GetChild(i);
                break;
            }
            
        }

        if (target == null)
            return;


        GameObject NewChess= Instantiate(Resources.Load("Prefabs/Characters/"+chessNames[(int)Random.Range(0, 5)]), target.position+new Vector3(0,2,0), Quaternion.identity) as GameObject;
        NewChess.transform.parent=target;
        NewChess.GetComponent<ChessFSMManager>().level = 1;
        UpGrade(NewChess.GetComponent<ChessFSMManager>());
    }
    public void InitializingWithInfo(int level, string name)
    {
        Transform target = null;
        for (int i = 0; i < 8; i++)
        {
            if (Store.transform.GetChild(i).childCount == 0)
            {
                target = Store.transform.GetChild(i);
                break;
            }

        }

        if (target == null)
            return;


        GameObject NewChess = Instantiate(Resources.Load("Prefabs/Characters/" + name), target.position + new Vector3(0, 1.8f, 0), Quaternion.identity) as GameObject;
        NewChess.transform.parent=target;
        NewChess.GetComponent<ChessFSMManager>().level = level;
        NewChess.GetComponent<ChessFSMManager>().SetDefaultStat();
        NewChess.GetComponent<ChessFSMManager>().ID = PlayerIDSet.playerID;
        if (level<=1)
        UpGrade(NewChess.GetComponent<ChessFSMManager>());
    }

    public void UpGrade(ChessFSMManager chess)
    {
        List<GameObject> upTargets =new List<GameObject>();
        int findLevel=chess.level;
        string findName=chess.name; 

        for(int i=0; i<8; i++)
        {
            if (Store.transform.GetChild(i).childCount != 0)
            {
                if(Store.transform.GetChild(i).GetChild(0).GetComponent<ChessFSMManager>().level == chess.level
                    && Store.transform.GetChild(i).GetChild(0).GetComponent<ChessFSMManager>().name == chess.name)
                {
                    upTargets.Add(Store.transform.GetChild(i).GetChild(0).gameObject);
                }
            }
        }

        for (int i = 0; i < 16; i++)
        {
            if (tiles1.transform.GetChild(i).childCount != 0)
            {
                if (tiles1.transform.GetChild(i).GetChild(0).GetComponent<ChessFSMManager>().level == chess.level
                    && tiles1.transform.GetChild(i).GetChild(0).GetComponent<ChessFSMManager>().name == chess.name)
                {
                    upTargets.Add(tiles1.transform.GetChild(i).GetChild(0).gameObject);
                }
            }
        }
        for (int i = 0; i < 16; i++)
        {
            if (tiles2.transform.GetChild(i).childCount != 0)
            {
                if (tiles2.transform.GetChild(i).GetChild(0).GetComponent<ChessFSMManager>().level == chess.level
                    && tiles2.transform.GetChild(i).GetChild(0).GetComponent<ChessFSMManager>().name == chess.name)
                {
                    upTargets.Add(tiles2.transform.GetChild(i).GetChild(0).gameObject);
                }
            }
        }

        if(upTargets.Count>=3)
        {
            for (int i = 0; i < 3; i++)
            {
                upTargets[i].transform.SetParent(null);
                upTargets[i].GetComponent<ChessFSMManager>().RemoveList();
                Destroy(upTargets[i]);
            }
            InitializingWithInfo(findLevel+1, findName);
        }

        upTargets=null;
    }

    public void ReRoll()
    {
        for(int i=0; i<5; i++)
        {
            if (slots[i].childCount > 0)
                Destroy(slots[i].GetChild(0).gameObject);

            GameObject.Instantiate(Resources.Load("Prefabs/UIs/UnitButton"), slots[i]);
        }
    }
}
