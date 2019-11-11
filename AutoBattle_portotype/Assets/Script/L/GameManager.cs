using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject Shop;

    public Text TimeText;

    public string sHeroName;

    public float timeLeft = 1.0f;

    public int Cost = 0;

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

            Shop.SetActive(true);
            ShopManager.instance.ReRoll();
            
            timeLeft = 30.0f;
        }
    }

    public void ShopReRoll()
    {
        iRandomNum = Random.Range(0, 100);

        if (PlayerManager.instance.iLevel == 1)
        {
            sHeroName = RandomRarity.instance.RandomDawn();
            Cost = 1;
        }
        else if(PlayerManager.instance.iLevel == 2)
        {
            if (iRandomNum < 80)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                Cost = 1;
            }
            else if (80 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                Cost = 2;
            }
        }
        else if (PlayerManager.instance.iLevel == 3)
        {
            if (iRandomNum < 70)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                Cost = 1;
            }
            else if (70 <= iRandomNum && iRandomNum < 90)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                Cost = 2;
            }
            else if (90 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                Cost = 3;
            }
        }
        else if (PlayerManager.instance.iLevel == 4)
        {
            if (iRandomNum < 50)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                Cost = 1;
            }
            else if (50 <= iRandomNum && iRandomNum < 85)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                Cost = 2;
            }
            else if (85 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                Cost = 3;
            }
        }
        else if (PlayerManager.instance.iLevel == 5)
        {
            if (iRandomNum < 40)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                Cost = 1;
            }
            else if (40 <= iRandomNum && iRandomNum < 75)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                Cost = 2;
            }
            else if (75 <= iRandomNum && iRandomNum < 95)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                Cost = 3;
            }
            else if (95<= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                Cost = 4;
            }
        }
        else if (PlayerManager.instance.iLevel == 6)
        {
            if (iRandomNum < 25)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                Cost = 1;
            }
            else if (25 <= iRandomNum && iRandomNum < 60)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                Cost = 2;
            }
            else if (60 <= iRandomNum && iRandomNum < 89)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                Cost = 3;
            }
            else if (89 <= iRandomNum && iRandomNum < 99)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                Cost = 4;
            }
            else if (99 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
                Cost = 5;
            }
        }
        else if (PlayerManager.instance.iLevel == 7)
        {
            if (iRandomNum < 20)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                Cost = 1;
            }
            else if (20 <= iRandomNum && iRandomNum < 40)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                Cost = 2;
            }
            else if (40 <= iRandomNum && iRandomNum < 70)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                Cost = 3;
            }
            else if (70 <= iRandomNum && iRandomNum < 95)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                Cost = 4;
            }
            else if (95 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
                Cost = 5;
            }
        }
        else if (PlayerManager.instance.iLevel == 8)
        {
            if (iRandomNum < 10)
            {
                sHeroName = RandomRarity.instance.RandomDawn();
                Cost = 1;
            }
            else if (10 <= iRandomNum && iRandomNum < 30)
            {
                sHeroName = RandomRarity.instance.RandomSunrise();
                Cost = 2;
            }
            else if (30 <= iRandomNum && iRandomNum < 60)
            {
                sHeroName = RandomRarity.instance.RandomLight();
                Cost = 3;
            }
            else if (60 <= iRandomNum && iRandomNum < 90)
            {
                sHeroName = RandomRarity.instance.RandomSunset();
                Cost = 4;
            }
            else if (90 <= iRandomNum)
            {
                sHeroName = RandomRarity.instance.RandomTwilight();
                Cost = 5;
            }
        }
    }
}
