using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGetSet : MonoBehaviour
{
    public Transform chess;
    public Transform tile;

    private Vector3 startPos;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, 1000, 512))
            {
                chess = hit.rigidbody.transform;
                startPos = chess.position;
                chess.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if(chess!=null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, 1024))
                {
                    chess.position = hit.point + new Vector3(0, 2, 0);
                    tile = hit.rigidbody.transform;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (chess != null)
            {
                if (tile != null)
                {
                    chess.position = tile.position + new Vector3(0, 2, 0);
                    chess.GetComponent<Rigidbody>().isKinematic = false;
                    chess.gameObject.layer = 0;
                }
                else
                    chess.position = startPos;
            }
            chess = null;
            tile = null;
        }
    }
}
