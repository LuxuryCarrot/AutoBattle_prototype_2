using System.Collections;
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

    private string StageName;

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

        if (Stage == CurStage.PREPARING)  //전투 준비 시간
        {
            if (iCurrState == 1)
            {
                timeLeft = 60.0f;
                StageName = "준비";
                Shop.SetActive(true);
                ShopManager.instance.ReRoll();
                iCurrState = 2;
            }

            if (timeLeft < 0)
            {
                Stage = CurStage.COMPAT;
            }
        }
        else if (Stage == CurStage.COMPAT) //전투 돌입 
        {
            if (iCurrState == 2)
            {
                timeLeft = 120.0f;
                StageName = "전투";
                ++iRoundCount;
                bisRoundStarted = true;
                iCurrState = 3;
            }

            if (timeLeft < 0)
            {
                bisRoundStarted = false;
                Stage = CurStage.FINISH;
            }
        }
        else if (Stage == CurStage.FINISH) // 전투 마무리 시간
        {
            if (iCurrState == 3)
            {
                timeLeft = 15.0f;
                StageName = "마무리";
                PlayerManager.instance.iExp += 1;
                PlayerManager.instance.iBalance += 5;
                iCurrState = 1;
            }

            if (timeLeft < 0)
            {
                Stage = CurStage.PREPARING;
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
}
