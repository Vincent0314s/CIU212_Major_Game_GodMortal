﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private static PlayerController _i;
    public static PlayerController i {
        get {
            if (_i == null) {
                _i = FindObjectOfType<PlayerController>();
            }
            return _i;
        }
    }


    public CharacterMovement cm { get; private set; }
    public CharacterBaseValue cbv { get; private set; }

    public PlayerState playerState;

    private bool isGoingRight;
    private bool isGoingLeft;
    private bool isDashing;

    private Vector3 moveVector;
    [Space]
    [Header("Dash")]
    public float dashDistance = 5;
    private float dashTimer;
    [Range(0.25f,0.4f)]
    public float dashDuration = 0.3f;

    [Space]
    [Header("KeyCode")]
    public KeyCode key_LightAttack;
    public KeyCode key_HeavyAttack;
    public KeyCode key_Dash;
    

    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        dashTimer = dashDuration;
    }

    void Update()
    {
        InputSetting();
        //ActionInput();
        //StateSetting();
        //AnimDisplay();
    }

    public void Idle() {
        isGoingRight = Input.GetKey(KeyCode.D);
        isGoingLeft = Input.GetKey(KeyCode.A);

        cbv.IsLightAttacking(key_LightAttack);
        cbv.IsHeavyAttacking(key_HeavyAttack);
        ActionInput();
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    }

    public void Run() {

        isGoingRight = Input.GetKey(KeyCode.D);
        isGoingLeft = Input.GetKey(KeyCode.A);
        isDashing = Input.GetKeyDown(key_Dash);

        cbv.IsLightAttacking(key_LightAttack);
        cbv.IsHeavyAttacking(key_HeavyAttack);
        ActionInput();
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
        //cbv.anim.SetBool("isDash", isDashing);
        if (isDashing) {
            cbv.anim.SetTrigger("Dash");
        }
    }

    public void Dash() {
        DashForward();
    }

    public void DashExit() {
        cbv.rb.velocity = Vector3.zero;
    }

    void InputSetting() {

        if (playerState == PlayerState.Idle || playerState == PlayerState.Moving)
        {
            isGoingRight = Input.GetKey(KeyCode.D);
            isGoingLeft = Input.GetKey(KeyCode.A);
        }

        if (playerState == PlayerState.Moving)
            isDashing = Input.GetKeyDown(key_Dash);

        if (playerState == PlayerState.Idle || playerState == PlayerState.Moving)
        {
            cbv.IsLightAttacking(key_LightAttack);
            cbv.IsHeavyAttacking(key_HeavyAttack);
        }
        else if (playerState == PlayerState.Jump)
        {
            //AirAttack
        }


        //isOnGround
        cm.JumpState(Input.GetKeyDown(KeyCode.Space));
    }

    void StateSetting() {
        switch (playerState) {
            case PlayerState.Idle:

                //Moving
                if (isGoingLeft || isGoingRight) {
                    playerState = PlayerState.Moving;
                }

                //Jump
                if (cm.isJumping)
                {
                    playerState = PlayerState.Jump;
                }

                //Attack
                if (cbv.isLightAttacking) {
                    playerState = PlayerState.LightAttack;
                }
                if (cbv.isHeavyAttacking) {
                    playerState = PlayerState.HeavyAttack;
                }
                break;
            case PlayerState.Moving:

                //Moving
                if (!isGoingLeft && !isGoingRight) {
                    playerState = PlayerState.Idle;
                }

                //Jump
                if (cm.isJumping)
                {
                    playerState = PlayerState.Jump;
                }
                //Dash
                if (isDashing) {
                    playerState = PlayerState.Dash;
                }
                //Attack
                if (cbv.isLightAttacking)
                {
                    playerState = PlayerState.LightAttack;
                }
                if (cbv.isHeavyAttacking)
                {
                    playerState = PlayerState.HeavyAttack;
                }
                break;
            case PlayerState.Jump:

                //If is OnGround
                //playerState = PlayerState.Idle;

                break;
            case PlayerState.Dash:
                cm.StopMoving();
                DashForward();
                break;
        }
    }

    void ActionInput() {
        float moveX = 0;
        if (isGoingRight) moveX = 1;
        if (isGoingLeft) moveX = -1;
        moveVector = new Vector3(moveX, 0, 0).normalized;
        GetComponent<IMovement>().SetVelocity(moveVector);
    }

    void AnimDisplay() {
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
        cbv.anim.SetBool("isDash", isDashing);
    }

    void DashForward() {
        //if (dashTimer >= 0)
        //{
        //    dashTimer -= Time.deltaTime;
        //    cbv.rb.velocity = transform.right * dashDistance;
        //}
        //else {
        //    dashTimer = dashDuration;
        //    cbv.rb.velocity = Vector3.zero;
        //    //playerState = PlayerState.Idle;
        //}
        cbv.rb.velocity = transform.right * dashDistance;
    }
}
