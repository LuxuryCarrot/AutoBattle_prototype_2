using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    private List<GameObject> enemys = new List<GameObject>();
    public GameObject enemyBoard1;
    public GameObject enemyBoard2;

    private void Awake()
    {
        instance = this.GetComponent<EnemySpawner>();
    }

    public void EnemySpawn()
    {
        int randomSid = (int)Random.Range(0, 31);
        Transform target;
        if (randomSid <= 15)
        {
            target = enemyBoard1.transform.GetChild(randomSid);
        }
        else
            target = enemyBoard2.transform.GetChild(randomSid - 16);
        GameObject NewChess = Instantiate(Resources.Load("Prefabs/Characters/ParkWarrior"), target.position + new Vector3(0, 1.8f, 0), Quaternion.identity) as GameObject;
        NewChess.transform.parent = target;
        NewChess.GetComponent<ChessFSMManager>().level = 1;
        NewChess.GetComponent<ChessFSMManager>().ID = PlayerIDSet.AIID;
        NewChess.GetComponent<ChessFSMManager>().SetDefaultStat();
        enemys.Add(NewChess);
        NewChess.GetComponent<ChessFSMManager>().SetState(ChessStates.CHASE);
    }

    public void EnemyRoundEnd()
    {
        foreach(GameObject obj in enemys)
        {
            enemys.Remove(obj);
            Destroy(obj);
        }
    }
}
