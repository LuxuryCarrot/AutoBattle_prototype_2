using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int MaxHeroNumber = 8;

    public int iBalance;
    public int iExp;
    public int iLevel;
    public int iLevelUpEXp;
    public int iSlotCount;

    public bool isInventoryFull;
    public bool bMoneyLeft;

    public Text PlayerLevel;
    public Text PlayerExp;
    public Text PlayerLevelUpExp;
    public Text PlayerBalance;

    public string[] sHeroName;

    public Transform[] InventorySlotPos;

    private GameObject CloneHero;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerManager.instance = this;

        sHeroName = new string[MaxHeroNumber];

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

    public void SetHero(int _index, string _sHeroName)
    {
        sHeroName[_index] = _sHeroName;
        CloneHero = Instantiate(Resources.Load("Prefabs/Characters/" + _sHeroName), InventorySlotPos[_index].position, InventorySlotPos[_index].rotation) as GameObject;
        CloneHero.GetComponent<ChessInfo>().ichessNum = _index;
        CloneHero.GetComponent<ChessInfo>().isWaiting = true;
        CloneHero.GetComponent<ChessInfo>().sMyName = _sHeroName;
    }
}
