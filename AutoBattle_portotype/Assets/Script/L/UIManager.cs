using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject ShopUI;
    public bool isShopButtonClicked = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShopButtenPushed()
    {
        if (isShopButtonClicked)
        {
            ShopUI.SetActive(true);
            isShopButtonClicked = false;
        }
        else
        {
            ShopUI.SetActive(false);
            isShopButtonClicked = true;
        }
    }


}
