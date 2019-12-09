using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject ShopUI;

    private bool ShopActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ShopActive == false)
        {
            ShopUI.SetActive(true);
            ShopActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && ShopActive == true)
        {
            ShopUI.SetActive(false);
            ShopActive = false;
        }
        
    }

    public void ShopButtenPushed()
    {
        ShopUI.SetActive(true);
    }


    public void ShopEXIT()
    {
        ShopUI.SetActive(false);
    }
}
