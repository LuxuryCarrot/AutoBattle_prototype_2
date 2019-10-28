using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject inventoryFullErrorTEXT;
    public GameObject NoBalanceErrorTEXT;

    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    public GameObject Slot5;


    public int[] iShopSection = new int[5];


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.timeLeft > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("GAMEMANGER상속: " + GameManager.instance.randShop[i]);
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
