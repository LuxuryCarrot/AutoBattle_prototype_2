using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text TimeText;


    public float timeLeft = 1.0f;
    public int[] randShop = new int[5];

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        TimeText.text = ("남은 시간: ") + (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                randShop[i] = Random.Range(0, 5);
                Debug.Log("Number" + i + "=" + randShop[i]);
            }
            PlayerManager.instance.iExp += 1;
            PlayerManager.instance.iBalance += 5;
            
            timeLeft = 30.0f;
        }
    }
}
