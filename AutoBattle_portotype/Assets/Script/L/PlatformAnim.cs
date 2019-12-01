using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnim : MonoBehaviour
{
    public static PlatformAnim instance;
    public Animator anim;
    public int iPlatformParam;
    // Start is called before the first frame update
    void Start()
    {
        PlatformAnim.instance = this;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("PlatformParam", iPlatformParam);
    }
}
