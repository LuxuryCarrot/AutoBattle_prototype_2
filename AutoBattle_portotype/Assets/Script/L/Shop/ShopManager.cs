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

    private void Update()
    {
        if (GameManager.instance.timeLeft <= 1)
        {
            Debug.Log("Reroll");
            ReRoll();
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

        PlayerManager.instance.iBalance -= 2;

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
            Debug.Log("Slot" + i + "= " + SlotCost[i]);
        }
    }

    public void Slot1()
    {
        Slot[0].SetActive(false);
        PlayerManager.instance.iBalance -= SlotCost[0];
        Instantiate(Resources.Load("Prefabs/ShopTestPrefab/" + sHeroName[0]), PlayerManager.instance.InventorySlotPos[0].position, PlayerManager.instance.InventorySlotPos[0].rotation);
    }
    public void Slot2()
    {
        Slot[1].SetActive(false);
        PlayerManager.instance.iBalance -= SlotCost[1];
    }
    public void Slot3()
    {
        Slot[2].SetActive(false);
        PlayerManager.instance.iBalance -= SlotCost[2];
    }
    public void Slot4()
    {
        Slot[3].SetActive(false);
        PlayerManager.instance.iBalance -= SlotCost[3];
    }
    public void Slot5()
    {
        Slot[4].SetActive(false);
        PlayerManager.instance.iBalance -= SlotCost[4];
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
