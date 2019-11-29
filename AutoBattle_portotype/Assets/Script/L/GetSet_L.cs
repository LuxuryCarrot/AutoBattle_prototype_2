using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSet_L : MonoBehaviour
{
    public Transform chess;
    public Transform tile;

    public Transform store;

    private Vector3 startPos;

  

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, 1024))
            {
                chess = hit.transform;
                startPos = chess.position;
                if (chess.GetComponent<ChessInfo>().isWaiting == true)
                {
                    Debug.Log("out");
                    PlayerManager.instance.sInventory[chess.GetComponent<ChessInfo>().ichessNum] = null;
                    --PlayerManager.instance.iBenchSlotCount;
                    chess.GetComponent<ChessInfo>().ichessNum = 999;
                    chess.GetComponent<ChessInfo>().isWaiting = false;
                }
                else if (chess.GetComponent<ChessInfo>().isWaiting == false)
                {
                    PlayerManager.instance.sGameBord[chess.GetComponent<ChessInfo>().ichessNum] = null;
                    --PlayerManager.instance.iBordSlotCount;
                    chess.GetComponent<ChessInfo>().ichessNum = 999;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (chess != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 6144))
                {
                    chess.position = hit.point + new Vector3(0, 2, 0);
                    tile = hit.transform;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (chess != null && chess.GetComponent<ChessInfo>().ichessNum == 999)
            {
                if (tile.gameObject.layer == 12 && PlayerManager.instance.iBenchSlotCount < PlayerManager.instance.MaxHeroNumber)
                {
                    Debug.Log("in");
                    for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
                    {
                        if (PlayerManager.instance.sInventory[i] == null)
                        {
                            chess.position = tile.position + new Vector3(0, 2, 0);
                            chess.GetComponent<ChessInfo>().ichessNum = i;
                            PlayerManager.instance.sInventory[i] = chess.GetComponent<ChessInfo>().sMyName;
                            ++PlayerManager.instance.iBenchSlotCount;
                            chess.GetComponent<ChessInfo>().isWaiting = true;
                            chess.tag = "chess";
                            break;
                        }
                    }
                    //chess.position = startPos;
                }
                else if (tile.gameObject.layer == 12 && PlayerManager.instance.iBenchSlotCount >= PlayerManager.instance.MaxHeroNumber)
                {
                    chess.position = startPos;
                }
                else if (tile != null && tile.gameObject.layer != 12)
                {
                    if (PlayerManager.instance.iBordSlotCount < PlayerManager.instance.iLevel)
                    {
                        for (int i = 0; i < PlayerManager.instance.iLevel; i++)
                        {
                            if (PlayerManager.instance.sGameBord[i] == null)
                            {
                                chess.position = tile.position + new Vector3(0, 2, 0);
                                chess.GetComponent<ChessInfo>().ichessNum = i;
                                PlayerManager.instance.sGameBord[i] = chess.GetComponent<ChessInfo>().sMyName;
                                //Debug.Log(PlayerManager.instance.sGameBord[i] + "!");
                                ++PlayerManager.instance.iBordSlotCount;
                                chess.GetComponent<ChessInfo>().isWaiting = false;
                                //chess.GetComponent<ChessFSMManager>().Settled();
                                chess.tag = "chess";
                                //chess.gameObject.layer = 0;
                            }
                        }
                    }
                }
                else
                {
                    chess.position = startPos;
                }

            }
            chess = null;
            tile = null;
        }
    }

}