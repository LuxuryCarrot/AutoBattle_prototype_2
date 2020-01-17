using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIdentity : MonoBehaviour
{
    public static string[] icons 
        = new string[5] { "Sword", "Shield", "Whip", "Whip", "Whip" };
    public static string[] prefabs
        = new string[5] { "ParkWarrior", "ParkShield", "Archer", "Mage", "Ranger" };
   
    

    private Image spriteImg;
    private int info;
    public ChessInitialize store;

    private void Awake()
    {
        info = (int)Random.Range(0, 5);
        
        store = GameObject.FindGameObjectWithTag("StoreFunction").GetComponent<ChessInitialize>();
        gameObject.GetComponent<Image>().sprite =
            store.images[info];
    }

    public void PurchaseButtonOn()
    {
        store.InitializingWithInfo(1, prefabs[info]);
        Destroy(gameObject);
    }

    
}
