using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSet_L : MonoBehaviour
{
    public Transform chess;
    public Transform tile;

    public Transform store;

    private Vector3 startPos;

    public GameObject MyPlatform;


    public GameObject tileEff;
    

    private void Awake()
    {
        tileEff.SetActive(false);
    }
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
                    PlayerManager.instance.Inventory[chess.GetComponent<ChessInfo>().ichessNum] = null;
                    --PlayerManager.instance.iBenchSlotCount;
                    chess.GetComponent<ChessInfo>().ichessNum = 999;
                    chess.GetComponent<ChessInfo>().isWaiting = false;
                }
                else if (chess.GetComponent<ChessInfo>().isWaiting == false)
                {
                    if (GameManager.instance.Stage != CurStage.PREPARING)
                    {
                        chess = null;
                        startPos = Vector3.zero;
                        return;
                    }
                    PlayerManager.instance.GameBord[chess.GetComponent<ChessInfo>().ichessNum] = null;
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
                    tileEff.SetActive(true);
                    tileEff.transform.position = tile.position+new Vector3(0, 1.02f,0);
                      
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            tileEff.SetActive(false);
            if (chess != null && chess.GetComponent<ChessInfo>().ichessNum == 999)
            {
                if (tile.gameObject.layer == 12 && PlayerManager.instance.iBenchSlotCount < PlayerManager.instance.MaxHeroNumber)
                {
                    //Debug.Log("in");
                    //for (int i = 0; i < PlayerManager.instance.MaxHeroNumber; i++)
                    //{
                    //    if (PlayerManager.instance.Inventory[i] == null)
                    //    {
                    //        chess.position = tile.position + new Vector3(0, 2, 0);
                    //        chess.GetComponent<ChessInfo>().ichessNum = i;

                    //            PlayerManager.instance.Inventory[i] = chess.gameObject;
                    //            ++PlayerManager.instance.iBenchSlotCount;

                    //        chess.GetComponent<ChessInfo>().isWaiting = true;
                    //        chess.tag = "Untagged";
                    //        chess.GetComponent<ChessFSMManager>().BenchIn();
                    //        break;
                    //    }
                    //}
                    //chess.position = startPos;

                    int tilego = (int)(tile.transform.position.x / 2) % 8;

                    if (PlayerManager.instance.Inventory[tilego]==null)
                    {
                        chess.position = tile.position + new Vector3(0, 2, 0);
                        chess.GetComponent<ChessInfo>().ichessNum = tilego;
                        PlayerManager.instance.Inventory[tilego] = chess.gameObject;
                        if(chess.tag=="chess")
                          ++PlayerManager.instance.iBenchSlotCount;
                        chess.GetComponent<ChessInfo>().isWaiting = true;
                        chess.tag = "Untagged";
                        
                        chess.GetComponent<ChessFSMManager>().BenchIn();
                        
                    }
                    else if(chess.tag=="chess")
                    {
                        GameObject changeChess = PlayerManager.instance.Inventory[tilego];
                        changeChess.transform.position = startPos+ new Vector3(0, 2, 0);
                        changeChess.GetComponent<ChessFSMManager>().Settled();
                        
                        changeChess.tag = "chess";
                        ++PlayerManager.instance.iBordSlotCount;
                        changeChess.GetComponent<ChessFSMManager>().EnQueueThis();
                        changeChess.GetComponent<ChessInfo>().isWaiting = false;

                        chess.position = tile.position + new Vector3(0, 2, 0);
                        chess.GetComponent<ChessInfo>().ichessNum = tilego;
                        PlayerManager.instance.Inventory[tilego] = chess.gameObject;
                         
                        chess.GetComponent<ChessInfo>().isWaiting = true;
                        chess.tag = "Untagged";

                        chess.GetComponent<ChessFSMManager>().BenchIn();
                    }
                    else
                    {
                        ++PlayerManager.instance.iBenchSlotCount;
                        GameObject changeChess = PlayerManager.instance.Inventory[tilego];
                        changeChess.transform.position = startPos;
                        changeChess.GetComponent<ChessInfo>().ichessNum = (int)(startPos.x / 2) % 8;
                        PlayerManager.instance.Inventory[(int)(startPos.x / 2) % 8] = changeChess;

                        chess.GetComponent<ChessInfo>().isWaiting = true;
                        chess.position = tile.position + new Vector3(0, 2, 0);
                        chess.GetComponent<ChessInfo>().ichessNum = tilego;
                        PlayerManager.instance.Inventory[tilego] = chess.gameObject;
                    }
                }
                else if (tile.gameObject.layer == 12 && PlayerManager.instance.iBenchSlotCount >= PlayerManager.instance.MaxHeroNumber)
                {
                    chess.position = startPos;
                }
                else if (tile != null && tile.gameObject.layer != 12 && GameManager.instance.Stage==CurStage.PREPARING)
                {

                    Debug.Log("passed?");
                    GameObject changeChess =null;
                    
                    foreach(GameObject obj in GameManager.instance.chessList)
                    {
                        if (obj.transform.position.x == tile.position.x && obj.transform.position.z == tile.position.z)
                            changeChess = obj;
                    }

                    if (chess.tag == "chess")
                    {
                        if(changeChess!=null)
                        {
                            for(int i=0; i<PlayerManager.instance.GameBord.Length; i++)
                                if(PlayerManager.instance.GameBord[i]==null)
                                {
                                    PlayerManager.instance.GameBord[i] = chess.gameObject;
                                    chess.GetComponent<ChessInfo>().ichessNum = i;
                                    break;
                                }

                            chess.position = changeChess.transform.position;
                            
                            changeChess.transform.position = startPos;
                        }
                        else
                        {
                            chess.position = tile.transform.position + new Vector3(0, 2, 0);
                            for (int i = 0; i < PlayerManager.instance.GameBord.Length; i++)
                                if (PlayerManager.instance.GameBord[i] == null)
                                {
                                    PlayerManager.instance.GameBord[i] = chess.gameObject;
                                    chess.GetComponent<ChessInfo>().ichessNum = i;
                                    break;
                                }
                        }
                        chess.transform.parent = MyPlatform.transform; // 게임 보드에 자식으로 넣어주는 코드. 자식으로는 들어가지는데 현재 전투에 돌입 시 착지하는 곳의 타일 위치가 맞지 않아 주석처리함.
                        ++PlayerManager.instance.iBordSlotCount;
                        Destroy(changeChess);
                        chess = null;
                        tile = null;
                        return;
                    }

                    if (changeChess != null)
                    {

                        ++PlayerManager.instance.iBordSlotCount;
                        changeChess.transform.position = startPos;
                        changeChess.GetComponent<ChessFSMManager>().BenchIn();
                        changeChess.GetComponent<ChessInfo>().isWaiting = true;
                        chess.GetComponent<ChessInfo>().ichessNum = changeChess.GetComponent<ChessInfo>().ichessNum;
                        changeChess.GetComponent<ChessInfo>().ichessNum = (int)(startPos.x / 2) % 8;
                        PlayerManager.instance.Inventory[(int)(startPos.x / 2) % 8] = changeChess;
                        changeChess.tag = "Untagged";

                        chess.position = tile.position + new Vector3(0, 2, 0);
                        chess.GetComponent<ChessFSMManager>().Settled();
                        chess.GetComponent<ChessInfo>().isWaiting = false;
                        chess.tag = "chess";
                        chess.GetComponent<ChessFSMManager>().EnQueueThis();
                        chess.transform.parent = MyPlatform.transform; // 게임 보드에 자식으로 넣어주는 코드. 자식으로는 들어가지는데 현재 전투에 돌입 시 착지하는 곳의 타일 위치가 맞지 않아 주석처리함.
                        for (int i=0;i< PlayerManager.instance.GameBord.Length; i++)
                        {
                            if(PlayerManager.instance.GameBord[i]==changeChess)
                            {
                                PlayerManager.instance.GameBord[i] = chess.gameObject;
                                chess.GetComponent<ChessInfo>().ichessNum = i;
                                break;
                            }
                        }
                        

                    }
                    else if (PlayerManager.instance.iBordSlotCount < PlayerManager.instance.iLevel && GameManager.instance.Stage != CurStage.COMPAT)
                    {
                        bool isPassed = false;
                       
                        for (int i = 0; i < PlayerManager.instance.iLevel; i++)
                        {


                            if (PlayerManager.instance.GameBord[i] == null)
                            {
                                ++PlayerManager.instance.iBordSlotCount;
                                chess.position = tile.position + new Vector3(0, 2, 0);
                                chess.GetComponent<ChessInfo>().ichessNum = i;
                                PlayerManager.instance.GameBord[i] = chess.gameObject;
                                //Debug.Log(PlayerManager.instance.sGameBord[i] + "!");
                                
                                chess.GetComponent<ChessInfo>().isWaiting = false;
                                chess.GetComponent<ChessFSMManager>().Settled();
                                chess.tag = "chess";
                                chess.GetComponent<ChessFSMManager>().EnQueueThis();
                                PlayerManager.instance.Inventory[(int)(startPos.x / 2) % 8] = null;
                                isPassed = true;
                                chess.transform.parent = MyPlatform.transform; // 게임 보드에 자식으로 넣어주는 코드. 자식으로는 들어가지는데 현재 전투에 돌입 시 착지하는 곳의 타일 위치가 맞지 않아 주석처리함.
                                Debug.Log("passed");
                                break;

                                //chess.gameObject.layer = 0;
                            }
                        }
                        if(!isPassed)
                        {
                            Debug.Log("passcheck");
                            chess.position = startPos;
                            chess.gameObject.GetComponent<ChessInfo>().isWaiting = true;
                            chess.gameObject.GetComponent<ChessInfo>().ichessNum = (int)(startPos.x / 2) % 8;
                            
                        }
                    }
                    else
                    {
                        chess.position = startPos;
                        chess.gameObject.GetComponent<ChessInfo>().isWaiting = true;
                        chess.gameObject.GetComponent<ChessInfo>().ichessNum = (int)(startPos.x / 2) % 8;
                        ++PlayerManager.instance.iBenchSlotCount;
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