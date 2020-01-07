using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGetSet : MonoBehaviour
{
    public Transform chess;
    public Transform tile;

    private Transform startPos;
    private GameManager gameManager;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 500, 1024))
            {

                chess = hit.transform;
                if (chess.gameObject.layer != 10)
                    chess = chess.GetChild(0);
                startPos = chess.parent;
                chess.parent.DetachChildren();
             
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if(chess!=null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 6144))
                {
                    chess.position = hit.point + new Vector3(0, 2, 0);
                    tile = hit.transform;
                }
                else
                {
                    tile = null;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (chess != null)
            {
                if(tile==null)
                {
                    chess.SetParent(startPos);
                    chess.transform.position = startPos.position += new Vector3(0, 1.5f, 0);
                }
                else if (tile != null)
                {
                    //int temp = 0;
                    if (tile.childCount == 0)
                    {
                        if (tile.gameObject.layer == 11)
                        {
                            //레벨제한 코드 추가할 곳
                        }
                        chess.GetComponent<ChessFSMManager>().Settled();
                        chess.parent = tile;
                        chess.position = tile.position + new Vector3(0, 2, 0);
                        
                        if (tile.gameObject.layer == 11)
                            chess.tag = "chess";
                        else
                            chess.tag = "Untagged";
                        //chess.gameObject.layer = 0;
                        //chess.GetComponent<ChessFSMManager>().EnQueueThis();
                    }
                    else
                    {
                        tile.GetChild(0).SetParent(startPos);
                        startPos.GetChild(0).position = startPos.position + new Vector3(0, 1.5f, 0);
                        startPos.GetChild(0).GetComponent<ChessFSMManager>().Settled();
                        if (startPos.gameObject.layer == 11)
                            chess.tag = "chess";
                        else
                            chess.tag = "Untagged";

                        chess.SetParent(tile);
                        chess.position = tile.position + new Vector3(0, 2, 0);
                        chess.GetComponent<ChessFSMManager>().Settled();
                        if (tile.gameObject.layer == 11)
                            chess.tag = "chess";
                        else
                            chess.tag = "Untagged";
                    }
                }
               
            }
            chess = null;
            tile = null;
        }
    }
}
