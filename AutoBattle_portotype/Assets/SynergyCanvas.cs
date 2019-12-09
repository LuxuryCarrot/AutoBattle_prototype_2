using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergyCanvas : MonoBehaviour
{
    public static bool warriorOn;
    public static bool mageOn;
    public static int warroirgage;

    public Button slot1;
    public Sprite sprite1;
    public Button slot2;
    public Sprite sprite2;
    public Button slot3;
    public Sprite sprite3;


    public static SynergyCanvas instance; 

    private void Awake()
    {
        slot1.enabled = false;
        slot1.image.color = Color.clear;
        slot2.enabled = false;
        slot2.image.color = Color.clear;
        slot3.enabled = false;
        slot3.image.color = Color.clear;
        instance = this;
       
    }

    public void WarriorSetSynergy(int level)
    {
        if (warriorOn == true)
            return;

        warriorOn = true;
        
        if(!slot1.enabled)
        {
            slot1.enabled = true;
            slot1.image.sprite = sprite1;
            slot1.image.color = Color.white;
            slot1.GetComponentInChildren<Text>().text = level.ToString();
        }
        else if(!slot2.enabled)
        {
            slot2.enabled = true;
            slot2.image.sprite = sprite1;
            slot2.image.color = Color.white;
            slot2.GetComponentInChildren<Text>().text = level.ToString();
        }
        else
        {
            slot3.enabled = true;
            slot3.image.sprite = sprite1;
            slot3.image.color = Color.white;
            slot3.GetComponentInChildren<Text>().text = level.ToString();
        }
        
    }

    public void MageSetSynergy(int level)
    {
        if (mageOn == true)
            return;

        mageOn = true;

        if (!slot1.enabled)
        {
            slot1.enabled = true;
            slot1.image.sprite = sprite3;
            slot1.image.color = Color.white;
            slot1.GetComponentInChildren<Text>().text = level.ToString();
        }
        else if (!slot2.enabled)
        {
            slot2.enabled = true;
            slot2.image.sprite = sprite3;
            slot2.image.color = Color.white;
            slot2.GetComponentInChildren<Text>().text = level.ToString();
        }
        else
        {
            slot3.enabled = true;
            slot3.image.sprite = sprite3;
            slot3.image.color = Color.white;
            slot3.GetComponentInChildren<Text>().text = level.ToString();
        }
    }

    public void RoundEnd()
    {
        slot1.enabled = false;
        slot1.image.color = Color.clear;
        slot2.enabled = false;
        slot2.image.color = Color.clear;
        slot3.enabled = false;
        slot3.image.color = Color.clear;
        
    }
}
