using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : CharacterMovement
{
    public int extraJumpCounts = 1;
    private int currentJumpTimes;
    private float fallingSpeed = 0;


    [Header("Debuff")]
    public bool isBeingDebuff;
    public float slowdownPercentage = 0.7f;
    public float slowdownDebuffTime = 5f;
    public float rootDebuffTime = 3.5f;
    private float currentDebuffTime;

    PlayerController pc;



    public override void Start()
    {
        base.Start();
        pc = GetComponent<PlayerController>();
        ResetJumpCounts();
    }

    private void Update()
    {
        Flip();
        //OnGround();
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
        cbv.anim.SetBool("isOnGround",isOnGround);
        cbv.anim.SetBool("Falling", IsFalling());
        cbv.anim.SetInteger("JumpTimes", currentJumpTimes);
        cbv.anim.SetFloat("FallingSpeed", Mathf.MoveTowards(0, 1, fallingSpeed));
    }
    
    public override void Jump()
    {
        base.Jump();
        fallingSpeed = 0;
    }
    public bool IsFalling() {
        if (cbv.rb.velocity.y < 0 && !isOnGround && !pc.isClimbing && !isOnSlope) {
            fallingSpeed += Time.deltaTime * 2f;
            return true;
        }
        return false;
    }

    public void DoubleJump() {
        currentJumpTimes = 0;
        fallingSpeed = 0;
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

    public void ResetJumpCounts() {
        currentJumpTimes = extraJumpCounts;
    }
}
