using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public static RoundManager Instance;

    public int wincount;

    public int Money;

    private void Awake()
    {
        Instance = this;
    }

    public void GoldGet(int round)
    {
        PlayerManager.instance.iBalance += 2;
        if (round < 3)
        {
            PlayerManager.instance.iBalance += (3 - round) * 5;
        }
        else if (round == 3)
            PlayerManager.instance.iBalance += 2;
    }

    public void EnemySpawn(int round)
    {
        if (round < 3)
            InstPrefab(0, 1);
        else
            InstPrefab(0, 2);

        if (round >= 3)
            InstPrefab((int)Mathf.Round(Random.Range(0, 1)), 1);

        if(round>=5)
            InstPrefab((int)Mathf.Round(Random.Range(0, 1)), 1);
    }

    public void InstPrefab(int a, int level)
    {
        GameObject inst = Instantiate(prefabs[a],
            new Vector3(Mathf.Round(Random.Range(0, 7)) * 2, 0.7f, Mathf.Round(Random.Range(4, 7)) * 2)
            , Quaternion.identity);
        inst.GetComponent<ChessFSMManager>().ID = PlayerIDSet.AIID;
        inst.tag = "chess";
        inst.GetComponent<ChessInfo>().iChessEvolutionRate = level;
        inst.GetComponent<ChessFSMManager>().SetDefaultStat();
        inst.GetComponent<ChessFSMManager>().SetState(ChessStates.CHASE);
        GameObject hpbar = Instantiate(Resources.Load("Prefabs/HPMPBars"), inst.transform.position, Quaternion.identity) as GameObject;
        hpbar.GetComponent<HPMPBarScripts>().target = inst;
    }

    public void RoundEnd()
    {
        Money = 0;
        GameObject[] counts = GameObject.FindGameObjectsWithTag("chess");
        int winner = 0;
        foreach(GameObject obj in counts)
        {
            int id = obj.GetComponent<ChessFSMManager>().ID;
            if (id == PlayerIDSet.playerID
                && obj.GetComponent<ChessFSMManager>().GetState() != ChessStates.DIE)
                winner++;
            else if (id == PlayerIDSet.AIID
                && obj.GetComponent<ChessFSMManager>().GetState() != ChessStates.DIE)
                winner--;
            else if (id == PlayerIDSet.AIID
                && obj.GetComponent<ChessFSMManager>().GetState() == ChessStates.DIE)
                PlayerManager.instance.iExp += 5;
        }
        wincount = winner;

        Money += winner * 2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
