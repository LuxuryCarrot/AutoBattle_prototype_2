using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGetSet : MonoBehaviour
{
    public Transform chess;
    public Transform tile;

    private Vector3 startPos;
    private GameManager gameManager;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 1000, 1024))
            {
                chess = hit.transform;
                startPos = chess.position;
                
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
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (chess != null)
            {
                if(tile.gameObject.layer==12)
                {
                    chess.position = startPos;
                }
                else if (tile != null)
                {
                    chess.position = tile.position + new Vector3(0, 2, 0);
                    chess.GetComponent<ChessFSMManager>().Settled();
                    chess.tag = "chess";
                    //chess.gameObject.layer = 0;
                    chess.GetComponent<ChessFSMManager>().EnQueueThis();
                }
               
            }
            chess = null;
            tile = null;
        }
    }
}
