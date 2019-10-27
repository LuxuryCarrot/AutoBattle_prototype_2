using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform Img;
    // Start is called before the first frame update
    void Start()
    {
        Img = GameObject.FindGameObjectWithTag("DragImg").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
