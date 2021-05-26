using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour,IMovement
{
    [Space]
    [Header("Movement")]
    [Range(3, 20)]
    public float MoveSpeed = 10f;
    private float currentMoveSpeed;
    [Range(0, 1)]
    public float turnSmoothValue = 0.35f;

    public bool CanFly;


    public bool isJumping { get; private set; }
    public bool isOnGround { get; private set; }

    [Space]
    [Header("Jump")]
    [Range(0,3)]
    public float detectGroundDistance = 1.5f;
    public float jumpForce = 5f;
    public float fallValue = 2.5f;
    [Range(0,5)]
    [Tooltip("How long can player stay in the air, 0 is longest")]
    public float heightToFall = 2f;
    public bool canDoubleJump;
    private int doubleJumpTimes = 1;
    public Vector3 groundDetectedOffset;

    public LayerMask groundLayer;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private CharacterBaseValue cbv;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        cbv = GetComponent<CharacterBaseValue>();
    }

    void Start() {
        currentMoveSpeed = MoveSpeed;
        doubleJumpTimes = 2;
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        moveDirection = velocityVector;
    }

    public bool JumpState(bool _jumpState) {
        isJumping = _jumpState;
        return isJumping;
    }

    public bool JumpState(KeyCode _jumpState)
    {
        isJumping = Input.GetKeyDown(_jumpState);
        return isJumping;
    }


    void Update() {
        Flip();
        OnGround();
        //Jump();
    }

    public void Flip() {
        if (moveDirection.x > 0)
        {
            //transform.eulerAngles = new Vector3(0, 0, 0);
            FacingRight(true);
        }
        else if (moveDirection.x < 0)
        {
            //transform.eulerAngles = new Vector3(0, 180, 0);
            FacingRight(false);
        }
    }

    public void FacingRight(bool _condition) {
        transform.eulerAngles = _condition ?new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
    }

    public void OnGround() {
        if (!CanFly) {
            isOnGround = Physics.Raycast(transform.position + groundDetectedOffset, Vector3.down, detectGroundDistance, groundLayer)
                 || Physics.Raycast(transform.position - groundDetectedOffset, Vector3.down, detectGroundDistance, groundLayer);

            if (isOnGround)
            {
                doubleJumpTimes = 2;
            }
        }
    }

    public void Jump() {
        if (canDoubleJump)
        {
            if (doubleJumpTimes > 0)
            {
                rb.velocity = new Vector3(cbv.rb.velocity.x, 0, cbv.rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJumpTimes--;
                StaminaController.ConsumeStamina(PlayerActionType.DoubleJump);
            }
        }
        else {
            //Single Jump
            if (isOnGround)
            {
                rb.velocity = new Vector3(cbv.rb.velocity.x, 0, cbv.rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                StaminaController.ConsumeStamina(PlayerActionType.Jump);
            }
        }
    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime);

        if (!CanFly) {
            if (!isOnGround)
            {
                if (rb.velocity.y < heightToFall)
                {
                    rb.velocity += Vector3.up * Physics.gravity.y * (fallValue - 1) * Time.fixedDeltaTime;
                }
            }
        }
    }

    public void StopMoving() {
        moveDirection = Vector3.zero;
        cbv.anim.SetFloat("Speed",0);
    }

    private void OnDrawGizmos()
    {
        //Display a line for jumping detection
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + groundDetectedOffset, transform.position + groundDetectedOffset + Vector3.down * detectGroundDistance);
        Gizmos.DrawLine(transform.position - groundDetectedOffset, transform.position - groundDetectedOffset + Vector3.down * detectGroundDistance);
    }


}
