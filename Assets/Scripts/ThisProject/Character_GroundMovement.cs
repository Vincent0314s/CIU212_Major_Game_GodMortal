using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_GroundMovement : MonoBehaviour, IMovement
{
    [Space]
    [Header("Movement")]
    [Range(3, 20)]
    public float MoveSpeed = 10f;
    public float currentMoveSpeed { get; private set; }

    protected Vector3 moveDirection;
    protected CharacterBaseValue cbv;

    //[Space]
    //[Header("Jump")]
    //[Range(0, 3)]
    //public float jumpForce = 5f;
    //public float fallValue = 2.5f;
    //[Range(0, 5)]
    //[Tooltip("How long can player stay in the air, 0 is longest")]
    //public float heightToFall = 2f;
    
    //[Space]
    //[Header("GroundCheck")]
    //public float detectGroundDistance = 1.5f;
    //public float groundDetectedOffsetX= 0.36f;
    //public float groundDetectedOffsetY;
    //public bool isOnGround { get; private set; }

    //public bool isOnSlope { get; private set; }
    //public float slopeDistance;
    //protected RaycastHit slopeHit;

    //public LayerMask groundLayer;


    void Awake()
    {
        cbv = GetComponent<CharacterBaseValue>();
    }

    public void Start() {
        ResetSpeed();
    }

    void Update() {
        Flip();
    }

    void FixedUpdate() {
        MoveToDirection();
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        moveDirection = velocityVector;
    }

    public void Flip() {
        if (moveDirection.x > 0)
        {
            FacingRight(true);
        }
        else if (moveDirection.x < 0)
        {
            FacingRight(false);
        }
    }

    public void FacingRight(bool _condition) {
        transform.eulerAngles = _condition ?new Vector3(0, 0, transform.eulerAngles.z) : new Vector3(0, 180, transform.eulerAngles.z);
    }

    public void MoveToDirection()
    {
        cbv.rb.MovePosition(transform.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime);
    }

    public void ResetSpeed() {
        currentMoveSpeed = MoveSpeed;
    }

    public void SetSpeed(float _newSpeed)
    {
        currentMoveSpeed = _newSpeed;
    }

    public void SetSpeedByPercentage(float _percentage) {
        currentMoveSpeed = currentMoveSpeed *= _percentage;
    }

    public void StopMoving()
    {
        moveDirection = Vector3.zero;
        cbv.anim.SetFloat("Speed", 0);
    }


    //public void OnSlope(bool _b) {
    //    if (_b)
    //    {
    //        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, slopeDistance, groundLayer)) {
    //            if (slopeHit.normal != Vector3.up)
    //            {
    //                isOnSlope = true;
    //            }
    //            else {
    //                isOnSlope = false;
    //            }
    //        }
    //    }
    //    else {
    //        isOnSlope = false;
    //    }
    //}

    //public void OnGround(bool _b) {
    //    if (_b)
    //    {
    //        isOnGround = Physics.Raycast(transform.position + new Vector3(groundDetectedOffsetX, groundDetectedOffsetY, 0), Vector3.down, detectGroundDistance, groundLayer)
    //                || Physics.Raycast(transform.position - new Vector3(groundDetectedOffsetX, -groundDetectedOffsetY, 0), Vector3.down, detectGroundDistance, groundLayer);
    //        //if (Physics.Raycast(transform.position,Vector3.down, out slopeHit, slopeDistance, groundLayer)) {
    //        //    //cbv.rb.AddForce((transform.position - slopeHit.point).normalized * 9.8f);
    //        //}

    //    }
    //    else {
    //        isOnGround = false;
    //    }

    //}


    //public virtual void FixedUpdate() {
    //    cbv.rb.MovePosition(transform.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime);

    //    //if (!isOnGround)
    //    //{
    //    //    if (cbv.rb.velocity.y < heightToFall)
    //    //    {
    //    //        cbv.rb.velocity += Vector3.up * Physics.gravity.y * (fallValue - 1) * Time.fixedDeltaTime;
    //    //    }
    //    //}
    //}

}
