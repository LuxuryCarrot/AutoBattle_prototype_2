using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessJump : ChessFSMParent
{

    private Vector3 moveVec;
    public float gravity;

    private float delta;
    private float xSpeed;
    private float ySpeed;
    private Vector3 movevecNormaled;

    public override void BeginState()
    {
        base.BeginState();
        moveVec = manager.target.position - transform.position;
        moveVec.y = 0;
        Debug.Log(Vector3.Magnitude(moveVec));
        delta = Vector3.Magnitude(moveVec) * 0.4f;
        movevecNormaled = Vector3.Normalize(moveVec);

        xSpeed = Mathf.Sqrt(delta * gravity);
        ySpeed = xSpeed;
    }

    private void Update()
    {
        transform.position += new Vector3(xSpeed * movevecNormaled.x * Time.deltaTime,
              ySpeed * Time.deltaTime,
              xSpeed * movevecNormaled.z * Time.deltaTime);

        ySpeed -= gravity * Time.deltaTime;


        if (transform.position.y <= 1.5)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
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
