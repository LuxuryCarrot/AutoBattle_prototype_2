using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dawn : MonoBehaviour
{
    string sTanker = "TANK";
    GameObject m_Dawn;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.iBalance == 10)
        {
            Instantiate(Resources.Load("Prefabs/" + sTanker), transform.position, transform.rotation);
        }
    }
}
