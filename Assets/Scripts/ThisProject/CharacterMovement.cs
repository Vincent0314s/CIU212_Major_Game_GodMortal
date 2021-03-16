using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour,IMovement
{
    [Range(3, 20)]
    public float currentMoveSpeed = 10f;

    [Range(0, 1)]
    public float turnSmoothValue = 0.35f;

    private bool isJumping;

    public float jumpForce = 5f;
    public float fallValue = 2.5f;

    private Vector3 moveDirection;

    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        moveDirection = velocityVector;
    }

    public void JumpState(bool _jumpState) {
        isJumping = _jumpState;
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

        if (isJumping) {
            rb.velocity = Vector3.up * jumpForce;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallValue - 1) * Time.deltaTime;
        }
    }
}
