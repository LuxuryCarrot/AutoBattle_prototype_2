using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public PlayerManager Player;

    public GameObject inventoryFullErrorTEXT;
    public GameObject NoBalanceErrorTEXT;

    public int[] iShopSection = new int[5];


    private GameManager GamemgrScript;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        GamemgrScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GamemgrScript.timeLeft > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("GAMEMANGER상속: " + GamemgrScript.randShop[i]);
            }
        }
    }

    public void BuySomething()
    {
        if (!Player.isInventoryFull)
        {
            Debug.Log("Someting buy");
        }
        else
        {
            StartCoroutine("Error_InventoryFull");
        }
    }

    public void ExpBUY()
    {
        if (Player.bMoneyLeft)
        {
            Player.iMoney -= 5;
            Player.iExp += 5;
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
