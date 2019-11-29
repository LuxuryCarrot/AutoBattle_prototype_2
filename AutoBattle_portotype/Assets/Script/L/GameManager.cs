﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CurStage
{
    PREPARING,
    COMPAT,
    FINISH
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject inst;

    public GameObject Shop;

    public Text TimeText;
    public Text RoundText;

    public bool bisRoundStarted;

    public string sHeroName;

    public float timeLeft = 1.0f;

    public int iCost = 0;
    public int iCurrentRound = 0;
    public int iRoundCount = 0;

    CurStage Stage;

    private int iRandomNum;
    private int iCurrState;
    private int iSameHeroCount = 0;

    private string sEHeroName;
    private string StageName;

    public Queue<GameObject> chessQueue = new Queue<GameObject>();  //라운드 시작 시 체스를 재생시키는 큐
    public Queue<GameObject> nextRoundQueue = new Queue<GameObject>();
    public Queue<Vector3> nextRoundPos = new Queue<Vector3>();
    

    private void Awake()
    {
        GameManager.instance = this;
        bisRoundStarted = false;
        Stage = CurStage.PREPARING; 
        iCurrState = 1;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        RoundText.text = iRoundCount.ToString("0") + (" 라운드 ") + StageName + ("!");
        TimeText.text = ("남은 시간: ") + (timeLeft).ToString("0");

        if (Stage == CurStage.PREPARING)            // 전투 준비 시간
        {
            if (iCurrState == 1)                    // 전투 준비 라운드 돌입하면서 한번만 실행됨
            {

                //timeLeft = 60.0f // 원래 시간
                timeLeft = 10.0f;
                ++iRoundCount;                      // 현재 라운드 카운트
                StageName = "준비";
                Shop.SetActive(true);               // 상점 창 자동으로 띄우기
                ShopManager.instance.ReRoll();      // 상점 아이템 랜덤으로 배치
                iCurrState = 2;

                if(nextRoundQueue!=null)
                for(;nextRoundQueue.Count!=0;)
                {
                    GameObject nextR = nextRoundQueue.Dequeue();
                    nextR.transform.position = nextRoundPos.Dequeue();
                    nextR.SetActive(true);
                    nextR.GetComponent<ChessFSMManager>().EnQueueThis();
                }
                
            }

            if (timeLeft < 0)                       // 전투 준비 시간 끝나면 전투 라운드로 스테이지 변경.
            {
                Stage = CurStage.COMPAT;
            }
        }
        else if (Stage == CurStage.COMPAT)          // 전투 돌입 
        {
            if (iCurrState == 2)                    // 전투 라운드 돌입하면서 한번만 실행됨
            {
                //timeLeft = 120.0f //원래 시간
                timeLeft = 20.0f;
                StageName = "전투";
                bisRoundStarted = true;             // 전투에 들어갔는지 확인. -> 전투 중이면 벤치에서 게임판으로 캐릭터 옮기는거 불가능하게 막아야함.
                iCurrState = 3;

                GameObject insts = Instantiate(inst, new Vector3(10, 2.6f, 10), Quaternion.identity);
                insts.GetComponent<ChessFSMManager>().ID = PlayerIDSet.AIID;
                insts.tag = "chess";

                for(;chessQueue.Count!=0;)          //배치한 말들을 모두 재생시키고 다음 라운드에 불러올 수 있도록 저장하는 포문
                {
                    GameObject deqChess = chessQueue.Dequeue();
                    //GameObject enqueChess = new GameObject();
                    //enqueChess = Instantiate(deqChess, deqChess.transform);
                    //enqueChess.transform.position = deqChess.transform.position;
                    //enqueChess.SetActive(false);
                    nextRoundQueue.Enqueue(deqChess);
                    nextRoundPos.Enqueue(deqChess.transform.position);
                    deqChess.GetComponent<ChessFSMManager>().DeQueueThis();
                }
            }

            if (timeLeft < 0)
            {
                Stage = CurStage.FINISH;            // 전투 라운드 시간이 다 되어 전투 마무리 라운드로 스테이지 변경.
            }
        }
        else if (Stage == CurStage.FINISH)          // 전투 마무리 시간
        {
            if (iCurrState == 3)
            {
                bisRoundStarted = false;            // 전투 상태 해제 -> 벤치에서 게임판으로, 게임판에서 벤치로의 캐릭터 옮기는 것 활성화.
                //timeLeft = 15.0f; //원래 시간
                timeLeft = 5.0f;
                StageName = "마무리";
                PlayerManager.instance.iExp += 1;   
                PlayerManager.instance.iBalance += 5;
                iCurrState = 1;
                EvolutionCheck();
            }

            if (timeLeft < 0)
            {
                GameObject[] chesss = GameObject.FindGameObjectsWithTag("chess");
                if(chesss!=null)
                {
                    for(int i=0;i<chesss.Length;i++)
                    {
                        chesss[i].GetComponent<ChessFSMManager>().SetState(ChessStates.IDLE);
                        chesss[i].SetActive(false);
                        if (chesss[i].GetComponent<ChessFSMManager>().ID == PlayerIDSet.AIID)
                            Destroy(chesss[i]);
                    }
                }
                Stage = CurStage.PREPARING;         // 전투 마무리 시간이 다 되어 다음라운드의 전투 준비 라운드로 스테이지 변경.
            }
        }
    }

    public void ShopReRoll()
    {
        iRandomNum = Random.Range(0, 100);

        if (PlayerManager.instance.iLevel == 1)
        {
            sHeroName = RandomRarity.instance.RandomDawn();
            iCost = 1;
        }
        else if(PlayerManager.instance.iLevel == 2)
        {
            if (iRandomNum < 80)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                iCost = 1;
            }
            else if (80 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                iCost = 2;
            }
        }
        else if (PlayerManager.instance.iLevel == 3)
        {
            if (iRandomNum < 70)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                iCost = 1;
            }
            else if (70 <= iRandomNum && iRandomNum < 90)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                iCost = 2;
            }
            else if (90 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                iCost = 3;
            }
        }
        else if (PlayerManager.instance.iLevel == 4)
        {
            if (iRandomNum < 50)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                iCost = 1;
            }
            else if (50 <= iRandomNum && iRandomNum < 85)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                iCost = 2;
            }
            else if (85 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                iCost = 3;
            }
        }
        else if (PlayerManager.instance.iLevel == 5)
        {
            if (iRandomNum < 40)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                iCost = 1;
            }
            else if (40 <= iRandomNum && iRandomNum < 75)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                iCost = 2;
            }
            else if (75 <= iRandomNum && iRandomNum < 95)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                iCost = 3;
            }
            else if (95<= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                iCost = 4;
            }
        }
        else if (PlayerManager.instance.iLevel == 6)
        {
            if (iRandomNum < 25)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                iCost = 1;
            }
            else if (25 <= iRandomNum && iRandomNum < 60)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                iCost = 2;
            }
            else if (60 <= iRandomNum && iRandomNum < 89)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                iCost = 3;
            }
            else if (89 <= iRandomNum && iRandomNum < 99)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                iCost = 4;
            }
            else if (99 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
                iCost = 5;
            }
        }
        else if (PlayerManager.instance.iLevel == 7)
        {
            if (iRandomNum < 20)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                iCost = 1;
            }
            else if (20 <= iRandomNum && iRandomNum < 40)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                iCost = 2;
            }
            else if (40 <= iRandomNum && iRandomNum < 70)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                iCost = 3;
            }
            else if (70 <= iRandomNum && iRandomNum < 95)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                iCost = 4;
            }
            else if (95 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
                iCost = 5;
            }
        }
        else if (PlayerManager.instance.iLevel == 8)
        {
            if (iRandomNum < 10)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                iCost = 1;
            }
            else if (10 <= iRandomNum && iRandomNum < 30)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                iCost = 2;
            }
            else if (30 <= iRandomNum && iRandomNum < 60)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                iCost = 3;
            }
            else if (60 <= iRandomNum && iRandomNum < 90)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                iCost = 4;
            }
            else if (90 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
                iCost = 5;
            }
        }
    }


    public void EvolutionCheck()
    {
        for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
        {
            if (PlayerManager.instance.sInventory[i] != null)
            {
                sEHeroName = PlayerManager.instance.sInventory[i];
                for (int j = 0; j < PlayerManager.instance.MaxHeroNumber; j++)
                {
                    if (sEHeroName == PlayerManager.instance.sInventory[j])
                    {
                        ++iSameHeroCount;
                    }
                }
                if (bisRoundStarted == false)
                {
                    for (int k = 0; k < PlayerManager.instance.iLevel; k++)
                    {
                        if (sEHeroName == PlayerManager.instance.sGameBord[k])
                        {
                            ++iSameHeroCount;
                        }
                    }
                }

                if (iSameHeroCount >= 3)
                {
                    for (int a = 0; a < 3; a++)
                    {

                    }
                }
            }
            Debug.Log(sEHeroName + " count : " + iSameHeroCount);
            iSameHeroCount = 0;
            sEHeroName = null;
        }
    }
}
