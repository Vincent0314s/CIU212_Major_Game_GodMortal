using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : CharacterMovement
{
    public int doubleJumpTimes = 1;
    private int currentdoubleJumpTimes;

    [Header("Debuff")]
    public bool isBeingDebuff;
    public float slowdownPercentage = 0.7f;
    public float slowdownDebuffTime = 5f;
    public float rootDebuffTime = 3.5f;
    private float currentDebuffTime;
    [SerializeField]
    private Transform currentStandingPlatform;

    PlayerController pc;

    public override void Start()
    {
        base.Start();
        pc = GetComponent<PlayerController>();
        currentdoubleJumpTimes = doubleJumpTimes;
    }

    private void Update()
    {
        Flip();
        OnGround();
        if (isBeingDebuff) {
            if (currentDebuffTime > 0)
            {
                currentDebuffTime -= Time.deltaTime;
            }
            else {
                isBeingDebuff = false;
                RecoverSpeed();
                currentDebuffTime = 0;
            }
        }

        cbv.anim.SetFloat("VelocityY", cbv.rb.velocity.y);
    }

    public override void OnGround()
    {
        base.OnGround();
        currentdoubleJumpTimes = doubleJumpTimes;
    }

    public void DoubleJump() {
        //if (currentdoubleJumpTimes > 0 && !isOnGround)
        //{
        //    cbv.rb.velocity = new Vector3(cbv.rb.velocity.x, 0, cbv.rb.velocity.z);
        //    cbv.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //    doubleJumpTimes--;
        //}
        cbv.rb.velocity = new Vector3(cbv.rb.velocity.x, 0, cbv.rb.velocity.z);
        cbv.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isOnGround && !pc.isClimbing)
        {
            if (cbv.rb.velocity.y < heightToFall)
            {
                cbv.rb.velocity += Vector3.up * Physics.gravity.y * (fallValue - 1) * Time.fixedDeltaTime;
            }
        }
    }

    public void BeingSlowDown() {
        isBeingDebuff = true;
        currentDebuffTime = slowdownDebuffTime;
        SetSpeed(currentMoveSpeed *= slowdownPercentage);
    }

    public void RecoverSpeed() {
        isBeingDebuff = false;
        SetSpeed(MoveSpeed);
    }

    public void BeingRoot() {
        isBeingDebuff = true;
        currentDebuffTime = rootDebuffTime;
        SetSpeed(0);
    }
}
