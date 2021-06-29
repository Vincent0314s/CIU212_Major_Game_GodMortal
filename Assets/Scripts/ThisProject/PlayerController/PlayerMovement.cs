using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    //public int extraJumpCounts = 1;
    //private int currentJumpTimes;

    //BlendPoseValue
    private float fallingSpeed = 0;

    [Header("Debuff")]
    public bool isBeingDebuff;
    public float slowdownPercentage = 0.7f;
    public float slowdownDebuffTime = 5f;
    public float rootDebuffTime = 3.5f;
    private float currentDebuffTime;

    PlayerController pc;
    public Character_Jump cj { get; private set; }
    public Character_GroundMovement cgm { get; private set; }
    public Character_GroundCheck cgc { get; private set; }

    public void Start()
    {
        pc = GetComponent<PlayerController>();
        cj = GetComponent<Character_Jump>();
        cgc = GetComponent<Character_GroundCheck>();
        cgm = GetComponent<Character_GroundMovement>();
    }

    private void Update()
    {
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
        pc.pv.anim.SetBool("isOnGround",cgc.isOnGround);
        pc.pv.anim.SetBool("Falling", IsFalling());
        pc.pv.anim.SetInteger("JumpTimes", cj.currentJumpTimes);
        pc.pv.anim.SetFloat("FallingSpeed", Mathf.MoveTowards(0, 1, fallingSpeed));
    }
    
    //public void Jump()
    //{
    //    fallingSpeed = 0;
    //}

    public bool IsFalling() {
        if (pc.pv.rb.velocity.y < 0 && !cgc.isOnGround && !pc.isClimbing && !cgc.isOnSlope) {
            fallingSpeed += Time.deltaTime * 2f;
            return true;
        }
        return false;
    }

    public void DoubleJump() {
        cj.SetJumpCounts(0);
        fallingSpeed = 0;
        cj.Jump();
    }

    public void FixedUpdate()
    {
        if (!cgc.isOnSlope && !pc.isClimbing)
        {
            cj.UpdateFallingGravity();
        }
    }

    public void BeingSlowDown() {
        isBeingDebuff = true;
        currentDebuffTime = slowdownDebuffTime;
        cgm.SetSpeedByPercentage(slowdownPercentage);
    }

    public void RecoverSpeed() {
        isBeingDebuff = false;
        cgm.ResetSpeed();
    }

    public void BeingRoot() {
        isBeingDebuff = true;
        currentDebuffTime = rootDebuffTime;
        cgm.SetSpeed(0);
    }

    public Vector3 DashDirection() {
        return Vector3.ProjectOnPlane(transform.right, cgc.slopeHit.normal);
        //return transform.right;
    }
}
