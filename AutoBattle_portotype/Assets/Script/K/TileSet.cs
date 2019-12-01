using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject storeTile;

    private void Awake()
    {
        bool isBlack = true;
        for(int i=0; i<8; i++)
        {
            for(int j=0; j<8; j++)
            {
                if (isBlack)
                {
                    GameObject tile = Instantiate(tiles[0], new Vector3(j*2,0,i*2), Quaternion.identity);
                    isBlack = false;
                }
                else
                {
                    GameObject tile = Instantiate(tiles[1], new Vector3(j*2, 0, i*2), Quaternion.identity);
                    isBlack = true;
                }
                

            }
            if (isBlack)
                isBlack = false;
            else
                isBlack = true;

            GameObject store = Instantiate(storeTile, new Vector3(i*2, 0, -4), Quaternion.identity);
        }
    }
}
