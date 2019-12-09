using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CurStage
{
    PREPARING,
    COMPAT,
    FINISH
}
public struct nextRound
{
    public GameObject obj;
    public Vector3 pos;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject inst;
    public GameObject inst2;

    public GameObject BillPanel;

    public GameObject Shop;

    public Text TimeText;
    public Text RoundText;

    public Text BillName;

    public Text BillResult;

    public Text BillList1;
    public Text BillList2;
    public Text BillList3;

    public Text BillTotal;


    public bool bisRoundStarted;

    public string sHeroName;

    public float timeLeft = 1.0f;

    public int iCost = 0;
    public int iCurrentRound = 0;
    public int iRoundCount = 0;

    public CurStage Stage;

    private int iRandomNum;
    private int iCurrState;
    private int iSameHeroCount = 0;
    private int EvRate;

    private string sEHeroName;
    private string StageName;

    public List<GameObject> chessList = new List<GameObject>();  //라운드 시작 시 체스를 재생시키는 큐
    public List<nextRound> nextRoundList = new List<nextRound>();
    

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

        if (timeLeft >= 0)
        {
            RoundText.text = iRoundCount.ToString("0") + (" 라운드 ") + StageName + ("!");
            TimeText.text = ("남은 시간: ") + (timeLeft).ToString("0");
        }

        if (Input.GetKey(KeyCode.S))
        {
            timeLeft -= 0.1f;
        }



        if (Stage == CurStage.PREPARING)            // 전투 준비 시간
        {
            if (iCurrState == 1)                    // 전투 준비 라운드 돌입하면서 한번만 실행됨
            {

                //timeLeft = 60.0f // 원래 시간
                BillPanel.SetActive(false);
                timeLeft = 30.0f;
                ++iRoundCount;                      // 현재 라운드 카운트
                StageName = "준비";
                RoundManager.Instance.GoldGet(iRoundCount);
                Shop.SetActive(true);               // 상점 창 자동으로 띄우기
                ShopManager.instance.ReRoll();      // 상점 아이템 랜덤으로 배치
                iCurrState = 2;

                

                //if(nextRoundQueue!=null)
                //for(;nextRoundQueue.Count!=0;)
                //{
                //    GameObject nextR = nextRoundQueue.Dequeue();
                //    nextR.transform.position = nextRoundPos.Dequeue();
                //    nextR.SetActive(true);
                //    nextR.GetComponent<ChessFSMManager>().EnQueueThis();
                //}
                if(nextRoundList!=null)
                {
                    foreach(nextRound part in nextRoundList)
                    {
                        part.obj.SetActive(true);
                        part.obj.transform.position = part.pos;
                        part.obj.GetComponent<ChessFSMManager>().EnQueueThis();
                        part.obj.GetComponent<ChessFSMManager>().SetDefaultStat();
                    }
                    nextRoundList.Clear();
                }

                SameHeroCheck();

            }

            if (timeLeft < 0)                       // 전투 준비 시간 끝나면 전투 라운드로 스테이지 변경.
            {
                StartCoroutine("COMPAT_Start");
            }
        }
        else if (Stage == CurStage.COMPAT)          // 전투 돌입 
        {
            if (iCurrState == 2)                    // 전투 라운드 돌입하면서 한번만 실행됨
            {
                //timeLeft = 120.0f //원래 시간
                timeLeft = 40.0f;
                StageName = "전투";
                bisRoundStarted = true;             // 전투에 들어갔는지 확인. -> 전투 중이면 벤치에서 게임판으로 캐릭터 옮기는거 불가능하게 막아야함.
                iCurrState = 3;

                

                if (chessList!=null)
                {
                    foreach(GameObject objs in chessList)
                    {
                        nextRound nextr;
                        nextr.obj = objs;
                        nextr.pos = objs.transform.position-new Vector3(0,0,0.5f);
                        nextRoundList.Add(nextr);
                        objs.GetComponent<ChessFSMManager>().DeQueueThis();
                        GameObject hpbar = Instantiate(Resources.Load("Prefabs/HPMPBars"), objs.transform.position, Quaternion.identity) as GameObject;
                        hpbar.GetComponent<HPMPBarScripts>().target = objs;
                    }
                    chessList.Clear();
                }

                RoundManager.Instance.EnemySpawn(iRoundCount);
            }


            GameObject[] dieChecks = GameObject.FindGameObjectsWithTag("chess");
            bool endCheck=true;
            bool enemyendCheck = true;
            foreach(GameObject obj in dieChecks)
            {
                if (obj.GetComponent<ChessFSMManager>().GetState() != ChessStates.DIE
                    && obj.GetComponent<ChessFSMManager>().ID == PlayerIDSet.playerID)
                    endCheck = false;

                if (obj.GetComponent<ChessFSMManager>().GetState() != ChessStates.DIE
                    && obj.GetComponent<ChessFSMManager>().ID == PlayerIDSet.AIID)
                    enemyendCheck = false;

            }

            if (endCheck || enemyendCheck)
                timeLeft = -0.1f;


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
                timeLeft = 10.0f;
                StageName = "마무리";
                PlayerManager.instance.iExp += 1;
                RoundManager.Instance.RoundEnd();
                Debug.Log(RoundManager.Instance.wincount);
                BillPanel.SetActive(true);
                if (RoundManager.Instance.wincount > 0)
                {
                    StartCoroutine("Win_Bill");
                }
                else if (RoundManager.Instance.wincount == 0)
                {
                    StartCoroutine("Lose_Bill");
                }
                else if (RoundManager.Instance.wincount < 0)
                {
                    StartCoroutine("Draw_Bill");
                }
                iCurrState = 1;
                //EvolutionCheck();
            }

            if (timeLeft < 0)
            {
                if (PlayerManager.instance.iBalance > 0)
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
                    StartCoroutine("PREPARING_Start");          // 전투 마무리 시간이 다 되어 다음라운드의 전투 준비 라운드로 스테이지 변경.
                }
                else
                {

                }
            }
        }
    }

    public void ShopReRoll()
    {
        iRandomNum = Random.Range(0, 9);

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

    public void SameHeroCheck()
    {
        for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
        {
            if (PlayerManager.instance.Inventory[i] != null)
            {
                sEHeroName = PlayerManager.instance.Inventory[i].GetComponent<ChessInfo>().sMyName;
                EvRate = PlayerManager.instance.Inventory[i].GetComponent<ChessInfo>().iChessEvolutionRate;

                for (int j = 0; j < PlayerManager.instance.MaxHeroNumber; j++)
                {
                    if (PlayerManager.instance.Inventory[j] != null)
                    {
                        if (sEHeroName == PlayerManager.instance.Inventory[j].GetComponent<ChessInfo>().sMyName 
                            && PlayerManager.instance.Inventory[j].GetComponent<ChessInfo>().iChessEvolutionRate == EvRate)
                        {
                            ++iSameHeroCount;
                            if (iSameHeroCount == 3)
                            {
                                Evolution();
                            }
                        }
                    }
                }
                if (bisRoundStarted == false)
                {
                    for (int k = 0; k < PlayerManager.instance.iLevel; k++)
                    {
                        if (PlayerManager.instance.GameBord[k] != null)
                        {
                            if (sEHeroName == PlayerManager.instance.GameBord[k].GetComponent<ChessInfo>().sMyName
                                && PlayerManager.instance.GameBord[k].GetComponent<ChessInfo>().iChessEvolutionRate == EvRate)
                            {
                                ++iSameHeroCount;
                                if (iSameHeroCount == 3)
                                {
                                    Evolution();
                                }
                            }
                        }
                    }
                }

              

            }
            //Debug.Log(sEHeroName + " count : " + iSameHeroCount + ", " + EvRate + "\n evolution count :" + i);
            iSameHeroCount = 0;
            sEHeroName = null;
        }
    }

    public void Evolution()
    {
        Debug.Log("evolved");
        for (int a = 0; iSameHeroCount!=0 && a<8; a++)
        {
            if (PlayerManager.instance.Inventory[a] != null)
            {
                if (PlayerManager.instance.Inventory[a].GetComponent<ChessInfo>().sMyName == sEHeroName
                    && PlayerManager.instance.Inventory[a].GetComponent<ChessInfo>().iChessEvolutionRate == EvRate)
                {
                    Destroy(PlayerManager.instance.Inventory[a]);
                    PlayerManager.instance.Inventory[a] = null;
                    --iSameHeroCount;
                }
            }
        }
        if (iSameHeroCount > 0)
        {
            for (int l = 0; l < iSameHeroCount; l++)
            {
                if (PlayerManager.instance.GameBord[l] != null)
                {
                    Debug.Log(PlayerManager.instance.GameBord[l].GetComponent<ChessInfo>().sMyName);
                    if (PlayerManager.instance.GameBord[l].GetComponent<ChessInfo>().sMyName == sEHeroName
                        && PlayerManager.instance.GameBord[l].GetComponent<ChessInfo>().iChessEvolutionRate == EvRate)
                    {
                        PlayerManager.instance.GameBord[l].GetComponent<ChessFSMManager>().BenchIn();
                        Destroy(PlayerManager.instance.GameBord[l]);
                        PlayerManager.instance.GameBord[l] = null;
                        --PlayerManager.instance.iBordSlotCount;
                        
                    }
                    
                }
            }
            iSameHeroCount = 0;
        }
        for (int z = 0; z < PlayerManager.instance.MaxHeroNumber; z++)
        {
            if (PlayerManager.instance.Inventory[z] == null && iSameHeroCount == 0)
            {
                PlayerManager.instance.SetHero(z, sEHeroName, EvRate + 1);
                break;
            }
        }
        Debug.Log(PlayerManager.instance.Inventory[0]);
    }

    IEnumerator COMPAT_Start()
    {
        PlatformAnim.instance.iPlatformParam = 1;
        yield return new WaitForSeconds(0.76f);
        Stage = CurStage.COMPAT;
        StopCoroutine("COMPAT_Start");
    }

    IEnumerator PREPARING_Start()
    {
        PlatformAnim.instance.iPlatformParam = 2;
        yield return new WaitForSeconds(0.76f);
        Stage = CurStage.PREPARING;
        StopCoroutine("PREPARING_Start");
    }

    IEnumerator Win_Bill()
    {
        BillName.text = ("영수증!");
        yield return new WaitForSeconds(0.5f);
        BillResult.text = ("게임 결과 : 승리!!");
        yield return new WaitForSeconds(0.5f);
        BillList1.text = ("상대방으로부터") + RoundManager.Instance.Money + ("원을 지급받았습니다.");
        yield return new WaitForSeconds(0.5f);
        BillList2.text = ("은행으로부터") + 5 + ("원을 후원받았습니다!");
        yield return new WaitForSeconds(0.5f);
        BillList3.text = ("잔액: ") + PlayerManager.instance.iBalance +("원") +  ("\n + 지급받은 금액: ") + RoundManager.Instance.Money + ("원") + ("\n + 후원받은 금액: ") + 5 + ("원");
        PlayerManager.instance.iBalance += 5;
        PlayerManager.instance.iBalance += RoundManager.Instance.Money;
        yield return new WaitForSeconds(0.5f);
        BillTotal.text = ("합계 : ") + PlayerManager.instance.iBalance + ("원!");
        StopCoroutine("Win_Bill");
    }

    IEnumerator Lose_Bill()
    {
        BillName.text = ("영수증!");
        yield return new WaitForSeconds(0.5f);
        BillResult.text = ("게임 결과 : 패배...");
        yield return new WaitForSeconds(0.5f);
        BillList1.text = ("상대방에게") + RoundManager.Instance.Money + ("원을 지불했습니다.");
        yield return new WaitForSeconds(0.5f);
        BillList2.text = ("은행으로부터") + 5 + ("원을 후원받았습니다!");
        yield return new WaitForSeconds(0.5f);
        BillList3.text = ("잔액: ") + PlayerManager.instance.iBalance + ("원") + ("\n + 지불한 금액: ") + RoundManager.Instance.Money + ("원") + ("\n + 후원받은 금액: ") + 5 + ("원");
        PlayerManager.instance.iBalance += 5;
        PlayerManager.instance.iBalance += RoundManager.Instance.Money;
        yield return new WaitForSeconds(0.5f);
        BillTotal.text = ("합계 : ") + PlayerManager.instance.iBalance + ("원!");
        StopCoroutine("Lose_Bill");
    }

    IEnumerator Draw_Bill()
    {
        BillName.text = ("영수증!");
        yield return new WaitForSeconds(0.5f);
        BillResult.text = ("게임 결과 : 무승부!");
        yield return new WaitForSeconds(0.5f);
        BillList2.text = ("은행으로부터") + 5 + ("원을 후원받았습니다!");
        yield return new WaitForSeconds(0.5f);
        BillList3.text = ("잔액: ") + PlayerManager.instance.iBalance + ("원") + ("\n + 후원받은 금액: ") + 5 + ("원");
        PlayerManager.instance.iBalance += 5;
        PlayerManager.instance.iBalance += RoundManager.Instance.Money;
        yield return new WaitForSeconds(0.5f);
        BillTotal.text = ("합계 : ") + PlayerManager.instance.iBalance + ("원!");
        StopCoroutine("Draw_Bill");
    }
}
