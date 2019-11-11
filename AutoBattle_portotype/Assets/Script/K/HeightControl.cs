using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightControl : MonoBehaviour
{


    private void Update()
    {
        if (transform.position.y <= 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().freezeRotation = true;
            
        }
    }
}
