using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject inventoryFullErrorTEXT;
    public GameObject NoBalanceErrorTEXT;

    public Text[] SlotText = new Text[5];

    public GameObject[] Slot = new GameObject[5];

    public int[] SlotCost = new int[5];

    private string[] sHeroName = new string[5];

    private void Awake()
    {
        ShopManager.instance = this;
    }

    public void ExpBUY()
    {
        if (PlayerManager.instance.bMoneyLeft)
        {
            PlayerManager.instance.iBalance -= 5;
            PlayerManager.instance.iExp += 5;
        }
        else
        {
            StartCoroutine("Error_NoBalance");
        }
    }

    public void ReRoll()
    {
        for (int i = 0; i < 5; i++)
        {
            Slot[i].SetActive(true);
        }

        for (int i = 0; i < 5; i++)
        {
            GameManager.instance.ShopReRoll();
            SlotCost[i] = GameManager.instance.Cost;
            SlotText[i].text = GameManager.instance.sHeroName + "\n" + "Cost = " + SlotCost[i];
            sHeroName[i] = GameManager.instance.sHeroName;
        }
    }

    public void ReRoll_Button()
    {
        PlayerManager.instance.iBalance -= 2;
        ReRoll();
    }

    public void Slot1()
    {
        if (SlotCost[0] <= PlayerManager.instance.iBalance)
        {
            for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
            {
                if (PlayerManager.instance.sHeroName[i] == null)
                {
                    BuyHero(i, 0);
                    Slot[0].SetActive(false);
                    break;
                }
            }
        }
        else
        {
            StartCoroutine("Error_NoBalance");
        }
    }
    public void Slot2()
    {
        if (SlotCost[1] <= PlayerManager.instance.iBalance)
        {
            for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
            {
                if (PlayerManager.instance.sHeroName[i] == null)
                {
                    BuyHero(i, 1);
                    Slot[1].SetActive(false);
                    break;
                }
            }
        }
        else
        {
            StartCoroutine("Error_NoBalance");
        }
    }
    public void Slot3()
    {
        if (SlotCost[2] <= PlayerManager.instance.iBalance)
        {
            for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
            {
                if (PlayerManager.instance.sHeroName[i] == null)
                {
                    BuyHero(i, 2);
                    Slot[2].SetActive(false);
                    break;
                }
            }
        }
        else
        {
            StartCoroutine("Error_NoBalance");
        }
    }
    public void Slot4()
    {
        if (SlotCost[3] <= PlayerManager.instance.iBalance)
        {
            for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
            {
                if (PlayerManager.instance.sHeroName[i] == null)
                {
                    BuyHero(i, 3);
                    Slot[3].SetActive(false);
                    break;
                }
            }
        }
        else
        {
            StartCoroutine("Error_NoBalance");
        }
    }
    public void Slot5()
    {
        if (SlotCost[4] <= PlayerManager.instance.iBalance)
        {
            for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
            {
                if (PlayerManager.instance.sHeroName[i] == null)
                {
                    BuyHero(i, 4);
                    Slot[4].SetActive(false);
                    break;
                }
            }
        }
    }
    private void BuyHero(int _SlotPosIndex, int _ShopSlotNum)
    {
        PlayerManager.instance.iBalance -= SlotCost[_ShopSlotNum];
        PlayerManager.instance.iSlotCount++;
        PlayerManager.instance.SetHero(_SlotPosIndex, sHeroName[_ShopSlotNum]);
    }

    IEnumerator Error_InventoryFull()
    {
        inventoryFullErrorTEXT.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        inventoryFullErrorTEXT.SetActive(false);
        StopCoroutine("Error_InventoryFull");
    }

    IEnumerator Error_NoBalance()
    {
        NoBalanceErrorTEXT.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        NoBalanceErrorTEXT.SetActive(false);
        StopCoroutine("Error_NoBalance");
    }
}
