using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessJump : ChessFSMParent
{

    private Vector3 moveVec;
    private float gravity;

    private float delta;
    private float xSpeed;
    private float ySpeed;
    private Vector3 movevecNormaled;

    public GameObject jumpDust;

    public override void BeginState()
    {
        base.BeginState();
        gravity = 10.0f;
        moveVec = manager.target.position - transform.position;
        moveVec.y = 0;
        Debug.Log(Vector3.Magnitude(moveVec));
        delta = Vector3.Magnitude(moveVec) * 0.45f;
        movevecNormaled = Vector3.Normalize(moveVec);

        xSpeed = Mathf.Sqrt(delta * gravity);
        ySpeed = xSpeed;
        manager.anim.SetInteger("Param", (int)ChessStates.JUMP);
    }

    private void Update()
    {
        transform.position += new Vector3(xSpeed * movevecNormaled.x * Time.deltaTime,
              ySpeed * Time.deltaTime,
              xSpeed * movevecNormaled.z * Time.deltaTime);

        ySpeed -= gravity * Time.deltaTime;


        if (transform.position.y < 2.0f)
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
            GameObject dust = Instantiate(jumpDust, manager.transform.position-new Vector3(0,1,0), Quaternion.identity);
            manager.SetState(ChessStates.ATTACK);
        }
    }

    public override void EndState()
    {
        
        delta = 0;
        xSpeed = 0;
        ySpeed = 0;
        movevecNormaled = Vector3.zero;
        moveVec = Vector3.zero;
        base.EndState();
    }
}
