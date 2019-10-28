using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : GameManager
{
    public PlayerManager Player;

    public GameObject inventoryFullErrorTEXT;

    public int[] ShopSection = new int[5];

    public bool full;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<PlayerManager>();

        Player.isInventoryFull = full;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("DDDDDDDDDDDD" + randShop[i]);
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
            StartCoroutine("InventoryFullError");
        }
    }

    IEnumerator InventoryFullError()
    {
        inventoryFullErrorTEXT.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        inventoryFullErrorTEXT.SetActive(false);
        StopCoroutine("InventoryFullError");
    }
}
