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
                    PlayerManager.instance.sInventory[chess.GetComponent<ChessInfo>().ichessNum] = null;
                    --PlayerManager.instance.iSlotCount;
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
            if (chess != null)
            {
                if (tile.gameObject.layer == 12 && PlayerManager.instance.iSlotCount < PlayerManager.instance.MaxHeroNumber)
                {
                    for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
                    {
                        if (PlayerManager.instance.sInventory[i] == null)
                        {
                            chess.position = tile.position + new Vector3(0, 2, 0);
                            chess.GetComponent<ChessInfo>().ichessNum = i;
                            PlayerManager.instance.sInventory[i] = chess.GetComponent<ChessInfo>().sMyName;
                            ++PlayerManager.instance.iSlotCount;
                            chess.GetComponent<ChessInfo>().isWaiting = true;
                            break;
                        }
                    }
                    //chess.position = startPos;
                }
                if (tile.gameObject.layer == 12 && PlayerManager.instance.iSlotCount >= PlayerManager.instance.MaxHeroNumber)
                {

                }
                else if (tile != null)
                {
                    chess.position = tile.position + new Vector3(0, 2, 0);
                    //chess.GetComponent<ChessFSMManager>().Settled();
                    chess.tag = "chess";
                    //chess.gameObject.layer = 0;
                }

            }
            chess = null;
            tile = null;
        }
    }
}
