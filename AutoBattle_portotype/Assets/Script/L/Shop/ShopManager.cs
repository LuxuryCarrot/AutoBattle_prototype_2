﻿
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
    public Button[] SlotButton = new Button[5];

    public Sprite Image1;
    public Sprite Image2;
    public Sprite Image3;
    public Sprite Image4;
    public Sprite Image5;


    public int[] SlotCost = new int[5];

    private string[] sHeroName = new string[5];

    private void Awake()
    {
        ShopManager.instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (PlayerManager.instance.iBalance >= 2)
            {
                PlayerManager.instance.iBalance -= 2;
                ReRoll();
            }
            else
            {
                StartCoroutine("Error_NoBalance");
            }
        }
    }

    public void ExpBUY()
    {
        if (PlayerManager.instance.iBalance >= 5 && PlayerManager.instance.iLevel < 9)
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
            SlotCost[i] = GameManager.instance.iCost;
            Color color;
            string heraldry;
            if (GameManager.instance.sHeroName == "ParkWarrior")
            {
                SlotButton[i].image.sprite = Image1;
                heraldry = "Starshyer";
                color = Color.black;
            }
            else if (GameManager.instance.sHeroName == "ParkShield")
            {
                SlotButton[i].image.sprite = Image2;
                heraldry = " ";
                color = Color.black;
            }
            else if(GameManager.instance.sHeroName =="Mage")
            {
                SlotButton[i].image.sprite = Image3;
                heraldry = "Lune";
                color = Color.blue;
            }
            else if(GameManager.instance.sHeroName=="Ranger")
            {
                SlotButton[i].image.sprite = Image5;
                heraldry = " ";
                color = Color.blue;
            }
            else
            {
                SlotButton[i].image.sprite = Image4;
                heraldry = "Selenthyor";
                color = Color.blue;
            }

           
            SlotText[i].text = GameManager.instance.sHeroName + "\n" + heraldry + "\n" + "Cost = " + SlotCost[i]+"Wn";
            SlotText[i].color = color;
            sHeroName[i] = GameManager.instance.sHeroName;
        }
    }

    public void ReRoll_Button()
    {
        if (PlayerManager.instance.iBalance >= 2)
        {
            PlayerManager.instance.iBalance -= 2;
            ReRoll();
        }
    }

    public void Slot1()
    {
        if (SlotCost[0] <= PlayerManager.instance.iBalance)
        {
            for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
            {
                if (PlayerManager.instance.Inventory[i] == null)
                {
                    BuyHero(i, 0);
                    Slot[0].SetActive(false);
                    GameManager.instance.SameHeroCheck();
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
                if (PlayerManager.instance.Inventory[i] == null)
                {
                    BuyHero(i, 1);
                    Slot[1].SetActive(false);
                    GameManager.instance.SameHeroCheck();
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
                if (PlayerManager.instance.Inventory[i] == null)
                {
                    BuyHero(i, 2);
                    Slot[2].SetActive(false);
                    GameManager.instance.SameHeroCheck();
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
                if (PlayerManager.instance.Inventory[i] == null)
                {
                    BuyHero(i, 3);
                    Slot[3].SetActive(false);
                    GameManager.instance.SameHeroCheck();
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
                if (PlayerManager.instance.Inventory[i] == null)
                {
                    BuyHero(i, 4);
                    Slot[4].SetActive(false);
                    GameManager.instance.SameHeroCheck();
                    break;
                }
            }
        }
    }
    private void BuyHero(int _SlotPosIndex, int _ShopSlotNum)
    {
        PlayerManager.instance.iBalance -= SlotCost[_ShopSlotNum];
        PlayerManager.instance.iBenchSlotCount++;
        PlayerManager.instance.SetHero(_SlotPosIndex, sHeroName[_ShopSlotNum], 1);
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
