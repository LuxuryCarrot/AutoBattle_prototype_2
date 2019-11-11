using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsDestroyer : MonoBehaviour
{
    public float temp;

    

    private void Update()
    {
        temp -= Time.deltaTime;
        if (temp <= 0)
            Destroy(gameObject);
    }
}
