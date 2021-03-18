using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour,IMovement
{
    [Range(3, 20)]
    public float MoveSpeed = 10f;
    private float currentMoveSpeed;

    [Range(0, 1)]
    public float turnSmoothValue = 0.35f;

    public bool isJumping { get; private set; }
    public bool isStopMoving { get; private set; }

    public float jumpForce = 5f;
    public float fallValue = 2.5f;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private CharacterBaseValue cbv;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        cbv = GetComponent<CharacterBaseValue>();
    }

    void Start() {
        currentMoveSpeed = MoveSpeed;
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        moveDirection = velocityVector;
    }

    public void JumpState(bool _jumpState) {
        isJumping = _jumpState;
    }



    void Update() {

        //transform.position += moveDirection * MoveSpeed * Time.deltaTime;
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

    void FixedUpdate() {
        rb.MovePosition(transform.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime);
    }

    public void StopMoving() {
        moveDirection = Vector3.zero;
        cbv.anim.SetFloat("Speed",0);
    }


}
