using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOnOff : MonoBehaviour
{
    public Canvas shop;

    public void ShopButtonOn()
    {
        if (shop.enabled)
            shop.enabled = false;
        else
            shop.enabled = true;
    }
}
