using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour,IMovement
{
    [Range(3, 20)]
    public float currentMoveSpeed = 10f;

    private float turnSmoothVelocity;
    [Range(0, 1)]
    public float turnSmoothValue = 0.35f;

    private bool isTurningRight;
    private bool isJumping;
    private bool isOnTheAir;

    public float jumpForce = 5f;
    public float fallValue = 2.5f;

    private Vector3 moveDirection;
    private Vector3 jumpDirection;

    public void SetVelocity(Vector3 velocityVector)
    {
        moveDirection = velocityVector;
    }

    public void SetJumpVelocity(Vector3 jumpVelocity) {
        jumpDirection = jumpVelocity;
    }

    void Update() {
        transform.position += moveDirection * currentMoveSpeed * Time.deltaTime;
        if (moveDirection.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveDirection.x < 0) {
            transform.eulerAngles = new Vector3(0,180,0);
        }

        
    }
}
