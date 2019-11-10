﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject inventoryFullErrorTEXT;
    public GameObject NoBalanceErrorTEXT;

    public Text[] SlotText = new Text[5];

    public GameObject[] Slot = new GameObject[5];

    public int[] iShopSection = new int[5];


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.timeLeft > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //Debug.Log("GAMEMANGER상속: " + GameManager.instance.randShop[i]);
            }
        }
    }

    public void BuySomething()
    {
        if (!PlayerManager.instance.isInventoryFull)
        {
            Debug.Log("Something buy");
        }
        else
        {
            StartCoroutine("Error_InventoryFull");
        }
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
            SlotText[i].text = GameManager.instance.sHeroName;
        }
    }

    public void Buy()
    {

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
