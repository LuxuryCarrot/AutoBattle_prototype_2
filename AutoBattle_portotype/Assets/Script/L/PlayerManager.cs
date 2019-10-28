using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int iMoney;
    public int iExp;
    public int iLevel;

    public bool isInventoryFull;
    public bool bMoneyLeft;

    // Start is called before the first frame update
    void Awake()
    {
        isInventoryFull = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (iMoney > 0)
        {
            bMoneyLeft = true;
        }
        else
        {
            bMoneyLeft = false;
        }
    }
}
