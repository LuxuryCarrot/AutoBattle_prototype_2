using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject storeTile;

    private void Awake()
    {
        Camera.main.transform.position = new Vector3(3.5f, 10f, -1.0f);
        Camera.main.transform.Rotate(45, 0, 0);
        bool isBlack = true;
        for(int i=0; i<8; i++)
        {
            for(int j=0; j<8; j++)
            {
                if (isBlack)
                {
                    GameObject tile = Instantiate(tiles[0], new Vector3(j,0,i), Quaternion.identity);
                    isBlack = false;
                }
                else
                {
                    GameObject tile = Instantiate(tiles[1], new Vector3(j, 0, i), Quaternion.identity);
                    isBlack = true;
                }
                

            }
            if (isBlack)
                isBlack = false;
            else
                isBlack = true;

            GameObject store = Instantiate(storeTile, new Vector3(i, 0, -2), Quaternion.identity);
        }
    }
}
