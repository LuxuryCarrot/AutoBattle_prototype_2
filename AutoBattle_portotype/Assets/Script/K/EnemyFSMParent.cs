using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSMParent : MonoBehaviour
{

    public FSMManager manager;
    

    private void Awake()
    {
        manager = GetComponent<FSMManager>();
    }

    public virtual void BeginState()
    {

    }
    public virtual void EndState()
    {

    }

}
