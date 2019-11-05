using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject ShopUI;

    public void ShopButtenPushed()
    {
        ShopUI.SetActive(true);
    }

    public void ShopEXIT()
    {
        ShopUI.SetActive(false);
    }
}
