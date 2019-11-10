using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int iBalance;
    public int iExp;
    public int iLevel;
    public int iLevelUpEXp;

    public bool isInventoryFull;
    public bool bMoneyLeft;

    public Text PlayerLevel;
    public Text PlayerExp;
    public Text PlayerLevelUpExp;
    public Text PlayerBalance;

    public Transform[] InventorySlotPos;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerManager.instance = this;

        isInventoryFull = false;

        iLevelUpEXp = 5;
        iLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLevel.text = ("레벨 :") + (iLevel).ToString("0");
        PlayerExp.text = ("경험치 :") + (iExp).ToString("0");
        PlayerBalance.text = ("잔액 :") + (iBalance).ToString("0");

        PlayerLevelUpExp.text = ("레벨업까지 필요한 경험치 :") + (iLevelUpEXp).ToString("0");

        if (iBalance > 0)
        {
            bMoneyLeft = true;
        }
        else
        {
            bMoneyLeft = false;
        }

        if (iExp >= iLevelUpEXp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        iLevelUpEXp += 5;
        iLevel += 1;
        iExp = 0;
    }
}
