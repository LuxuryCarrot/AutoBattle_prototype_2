using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundContinueManager : MonoBehaviour
{
    float time;
    public int current;
    public int level;
    public Text levelText;
    public Text timeText;
    public GameObject platform;
    public List<GameObject> chessList = new List<GameObject>();

    private void Awake()
    {
        time = 30;
        current = 0;
        level = 1;
        levelText.text = level.ToString();
    }

    private void Update()
    {
        if(current==0)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(time).ToString();
            if(time<=0)
            {
                platform.GetComponent<Animator>().SetInteger("PlatformParam", 1);
                time = 10;
                foreach(GameObject obj in chessList)
                {
                    obj.GetComponent<ChessFSMManager>().DeQueueThis();
                    obj.GetComponent<ChessFSMManager>().SetState(ChessStates.CHASE);
                }
                EnemySpawner.instance.EnemySpawn();
                current = 1;
            }
     
        }
        else if(current ==1)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(time).ToString();
            if (time<=0)
            {
                time = 5;
                foreach (GameObject obj in chessList)
                {
                    if(obj.GetComponent<ChessFSMManager>().GetState()!=ChessStates.DIE)
                       obj.GetComponent<ChessFSMManager>().SetState(ChessStates.IDLE);
                }
                current = 2;
            }
        }
        else if(current ==2)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(time).ToString();
            if (time<=0)
            {
                platform.GetComponent<Animator>().SetInteger("PlatformParam", 2);
                time = 30;
                foreach (GameObject obj in chessList)
                { 
                    obj.GetComponent<ChessFSMManager>().SetState(ChessStates.IDLE);
                    obj.transform.localPosition = new Vector3(0, 0, 0);
                }
                current = 0;
                level++;
                levelText.text = level.ToString();
                SynergyCanvas.instance.RoundEnd();
                EnemySpawner.instance.EnemyRoundEnd();
            }
        }

    }

    public bool CanSetInBoard()
    {
        if (level <= chessList.Count)
            return false;
        else
            return true;
    }
}
