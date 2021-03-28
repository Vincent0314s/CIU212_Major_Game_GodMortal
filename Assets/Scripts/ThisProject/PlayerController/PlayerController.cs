using System.Collections;
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
    public KeyCode key_Jump;


    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        dashTimer = dashDuration;
    }

    void Update()
    {
        
        Jump();
        //InputSetting();
        //ActionInput();
        //StateSetting();
        //AnimDisplay();
    }

  

    public void Idle() {
        MoveFunction();
        AttackInputFunction();
        //isGoingRight = Input.GetKey(KeyCode.D);
        //isGoingLeft = Input.GetKey(KeyCode.A);

        //cbv.IsLightAttacking(key_LightAttack);
        //cbv.IsHeavyAttacking(key_HeavyAttack);
        //cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    }

    public void Run() {
        MoveFunction();
        AttackInputFunction();
        DashInputFunction();
    }

    public void Jump() {
        //cbv.rb.velocity = new Vector3(cbv.rb.velocity.x,0,cbv.rb.velocity.z);
        //cbv.rb.AddForce(Vector3.up * 15f,ForceMode.Impulse);
        //cm.JumpState(key_Jump);
        if (Input.GetKeyDown(key_Jump)) {
            cm.Jump();
        }
    }

    public void Dash() {
        DashUpdate();
    }

    public void LightAttackInput()
    {
        cbv.IsLightAttacking(key_LightAttack);
    }

    public void HeavyAttackInput() {
        cbv.IsHeavyAttacking(key_HeavyAttack);
    }

    void MoveFunction() {

        isGoingRight = Input.GetKey(KeyCode.D);
        isGoingLeft = Input.GetKey(KeyCode.A);
        float moveX = 0;
        if (isGoingRight) moveX = 1;
        if (isGoingLeft) moveX = -1;
        moveVector = new Vector3(moveX, 0, 0).normalized;
        GetComponent<IMovement>().SetVelocity(moveVector);
        
        cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));

    }

    void AttackInputFunction() {
        cbv.IsLightAttacking(key_LightAttack);
        cbv.IsHeavyAttacking(key_HeavyAttack);

        if (cbv.isLightAttacking)
        {
            //cbv.anim.SetTrigger("LightAttackStart");
            cbv.anim.Play("LightAttack01");
        }
        if (cbv.isHeavyAttacking) {
            cbv.anim.Play("HeavyAttack01");
        }
    }



    void DashInputFunction() {
        isDashing = Input.GetKeyDown(key_Dash);
        if (isDashing)
        {
            cbv.anim.SetTrigger("Dash");
        }
    }
    void DashUpdate() {
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
    public void DashExit()
    {
        cbv.rb.velocity = Vector3.zero;
    }




    ///////////////////////Old

    //void AnimDisplay()
    //{
    //    cbv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    //    cbv.anim.SetBool("isDash", isDashing);
    //}

    //void InputSetting()
    //{

    //    if (playerState == PlayerState.Idle || playerState == PlayerState.Moving)
    //    {
    //        isGoingRight = Input.GetKey(KeyCode.D);
    //        isGoingLeft = Input.GetKey(KeyCode.A);
    //    }

    //    if (playerState == PlayerState.Moving)
    //        isDashing = Input.GetKeyDown(key_Dash);

    //    if (playerState == PlayerState.Idle || playerState == PlayerState.Moving)
    //    {
    //        cbv.IsLightAttacking(key_LightAttack);
    //        cbv.IsHeavyAttacking(key_HeavyAttack);
    //    }
    //    else if (playerState == PlayerState.Jump)
    //    {
    //        //AirAttack
    //    }


    //    //isOnGround
    //    cm.JumpState(Input.GetKeyDown(KeyCode.Space));
    //}

    //void StateSetting()
    //{
    //    switch (playerState)
    //    {
    //        case PlayerState.Idle:

    //            //Moving
    //            if (isGoingLeft || isGoingRight)
    //            {
    //                playerState = PlayerState.Moving;
    //            }

    //            //Jump
    //            if (cm.isJumping)
    //            {
    //                playerState = PlayerState.Jump;
    //            }

    //            //Attack
    //            if (cbv.isLightAttacking)
    //            {
    //                playerState = PlayerState.LightAttack;
    //            }
    //            if (cbv.isHeavyAttacking)
    //            {
    //                playerState = PlayerState.HeavyAttack;
    //            }
    //            break;
    //        case PlayerState.Moving:

    //            //Moving
    //            if (!isGoingLeft && !isGoingRight)
    //            {
    //                playerState = PlayerState.Idle;
    //            }

    //            //Jump
    //            if (cm.isJumping)
    //            {
    //                playerState = PlayerState.Jump;
    //            }
    //            //Dash
    //            if (isDashing)
    //            {
    //                playerState = PlayerState.Dash;
    //            }
    //            //Attack
    //            if (cbv.isLightAttacking)
    //            {
    //                playerState = PlayerState.LightAttack;
    //            }
    //            if (cbv.isHeavyAttacking)
    //            {
    //                playerState = PlayerState.HeavyAttack;
    //            }
    //            break;
    //        case PlayerState.Jump:

    //            //If is OnGround
    //            //playerState = PlayerState.Idle;

    //            break;
    //        case PlayerState.Dash:
    //            cm.StopMoving();
    //            DashUpdate();
    //            break;
    //    }
    //}
}
