using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text TimeText;

    public string sHeroName;

    public float timeLeft = 1.0f;

    private int iRandomNum;

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        TimeText.text = ("남은 시간: ") + (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            PlayerManager.instance.iExp += 1;
            PlayerManager.instance.iBalance += 5;
            
            timeLeft = 30.0f;
        }
    }

    public void ShopReRoll()
    {
        iRandomNum = Random.Range(0, 100);

        if (PlayerManager.instance.iLevel == 1)
        {
            sHeroName = RandomRarity.instance.RandomDawn();
        }
        else if(PlayerManager.instance.iLevel == 2)
        {
            if (iRandomNum < 80)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
            }
            else if (80 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
            }
        }
        else if (PlayerManager.instance.iLevel == 3)
        {
            if (iRandomNum < 70)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
            }
            else if (70 <= iRandomNum && iRandomNum < 90)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
            }
            else if (90 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomLight();
            }
        }
        else if (PlayerManager.instance.iLevel == 4)
        {
            if (iRandomNum < 50)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
            }
            else if (50 <= iRandomNum && iRandomNum < 85)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
            }
            else if (85 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomLight();
            }
        }
        else if (PlayerManager.instance.iLevel == 5)
        {
            if (iRandomNum < 40)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
            }
            else if (40 <= iRandomNum && iRandomNum < 75)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
            }
            else if (75 <= iRandomNum && iRandomNum < 95)
            {
                sHeroName = RandomRarity.instance.RandomLight();
            }
            else if (95<= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
            }
        }
        else if (PlayerManager.instance.iLevel == 6)
        {
            if (iRandomNum < 25)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
            }
            else if (25 <= iRandomNum && iRandomNum < 60)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
            }
            else if (60 <= iRandomNum && iRandomNum < 89)
            {
                sHeroName = RandomRarity.instance.RandomLight();
            }
            else if (89 <= iRandomNum && iRandomNum < 99)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
            }
            else if (99 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
            }
        }
        else if (PlayerManager.instance.iLevel == 7)
        {
            if (iRandomNum < 20)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
            }
            else if (20 <= iRandomNum && iRandomNum < 40)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
            }
            else if (40 <= iRandomNum && iRandomNum < 70)
            {
                sHeroName = RandomRarity.instance.RandomLight();
            }
            else if (70 <= iRandomNum && iRandomNum < 95)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
            }
            else if (95 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
            }
        }
        else if (PlayerManager.instance.iLevel == 8)
        {
            if (iRandomNum < 10)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
            }
            else if (10 <= iRandomNum && iRandomNum < 30)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
            }
            else if (30 <= iRandomNum && iRandomNum < 60)
            {
                sHeroName = RandomRarity.instance.RandomLight();
            }
            else if (60 <= iRandomNum && iRandomNum < 90)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
            }
            else if (90 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
            }
        }
    }
}
