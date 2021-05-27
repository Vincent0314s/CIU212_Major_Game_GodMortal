using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private static PlayerController _i;
    //public static PlayerController i {
    //    get {
    //        if (_i == null) {
    //            _i = FindObjectOfType<PlayerController>();
    //        }
    //        return _i;
    //    }
    //}

    public PlayerMovement pm { get; private set; }
    public PlayerValue pv { get; private set; }
    private PlayerAnimationEvent pae;

    public Levels whereisPlayer;
    public Vector3 respawonPosition;

    private bool isGoingRight;
    private bool isGoingLeft;
    private bool isGoingUp;
    private bool isGoingDown;

    private Vector3 moveVector;
    [Space]
    [Header("Dash")]
    public float dashDistance = 5;

    [Space]
    [Header("RangedPower")]
    public GameObject rangedPowerObject;
    public float rangedPowerForce = 20f;

    public Transform rope;
    public bool isClimbing { get; private set; }
    //public Transform rope { set; private get; }
    private Vector3 ropeDestination;
    [Space]
    [Header("KeyCode")]
    public KeyCode key_LightAttack;
    public KeyCode key_HeavyAttack;
    public KeyCode key_Dash;
    public KeyCode key_Jump;
    public KeyCode key_HealthPotion;
    public KeyCode key_StanimaPotion;
    public KeyCode key_RangedPower;
    public KeyCode key_Escape;
    public KeyCode key_Interact;

 

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pv = GetComponent<PlayerValue>();
        pae = GetComponentInChildren<PlayerAnimationEvent>();
    }

    public void InitPlayerController() {
        pv.anim.Play("Idle");
        transform.position = respawonPosition;
    }

    private void MoveHorizontally() {
        isGoingRight = Input.GetKey(KeyCode.D);
        isGoingLeft = Input.GetKey(KeyCode.A);
    }

    private void MoveVertically() {
        isGoingUp = Input.GetKey(KeyCode.W);
        isGoingDown = Input.GetKey(KeyCode.S);
    }

    public bool IsPressingDash() {
        if (pv.staminaSetting.CanConsumeStamina() && Input.GetKeyDown(key_Dash)) {
            pv.staminaSetting.ConsumeStamina(PlayerActionType.Dash);
            return true;
        }
        return false;
    }

    public bool IsPressingJump() {
        if (pv.staminaSetting.CanConsumeStamina() && Input.GetKeyDown(key_Jump))
        {
            return true;
        }
        return false;
    }

    public bool IsPressingLightAttack() {
        if (pv.staminaSetting.CanConsumeStamina() && Input.GetKeyDown(key_LightAttack))
        {
            pv.staminaSetting.ConsumeStamina(PlayerActionType.LightAttack);
            return true;
        }
        return false;
    }

    public bool IsPressingHeavyAttack()
    {
        if (pv.staminaSetting.CanConsumeStamina() && Input.GetKeyDown(key_HeavyAttack))
        {
            pv.staminaSetting.ConsumeStamina(PlayerActionType.HeavyAttack);
            return true;
        }
        return false;
    }

    public bool IsPressingRangedPower()
    {
        if (pv.staminaSetting.CanConsumeStamina() && Input.GetKeyDown(key_RangedPower))
        {
            pv.staminaSetting.ConsumeStamina(PlayerActionType.RangedPower);
            return true;
        }
        return false;
    }

    public bool IsPressingInteract()
    {
        return Input.GetKeyDown(key_Interact);
    }

    public bool IsPressingItemSlot_01() {
        if (ItemManager.i.CanUsingItems(Items.HealthPotion) && Input.GetKeyDown(key_HealthPotion)) {
            ItemManager.i.RemoveItems(Items.HealthPotion);
            return true;
        }
        return false;
    }

    public bool IsPressingItemSlot_02() {
        if (ItemManager.i.CanUsingItems(Items.StaminaPotion) && Input.GetKeyDown(key_StanimaPotion))
        {
            ItemManager.i.RemoveItems(Items.StaminaPotion);
            return true;
        }
        return false;
    }


    void Update()
    {
        if (!pv.healthSetting.IsDead) {
            if (IsPressingItemSlot_01())
            {
                pv.AddHP(ItemManager.i.healthPotionHealAmount);
            }

            if (IsPressingItemSlot_02()) { 
                //RecoverStanima
            }

            if (Input.GetKeyDown(key_Escape)) {
                GameFlowManager.i.OpenPauseMenu();
            }
        }
    }


    public void Idle_Update() {
        MoveFunction();
        AttackInputFunction();
        LaunchRangedPowerFunction();
        AttachToRopeFunction();
        JumpFunction();
    }

    public void Run_Update() {
        MoveFunction();
        AttackInputFunction();
        DashInputFunction();
        LaunchRangedPowerFunction();
        AttachToRopeFunction();
        JumpFunction();
    }

    public void Dash_Update()
    {
        pv.rb.velocity = transform.right * dashDistance;
    }

    public void Dash_Exit()
    {
        pv.rb.velocity = Vector3.zero;
    }
    public void JumpFunction() {
        if (IsPressingJump()) {
            pv.anim.SetTrigger("Jump");
        }
    }

    public void Jump_Enter() {
        pm.Jump();
    }

    public void Jump_Update() {
        MoveHorizontally();
        float moveX = 0;
        if (isGoingRight) moveX = 1;
        if (isGoingLeft) moveX = -1;
        moveVector = new Vector3(moveX, 0, 0).normalized;
        GetComponent<IMovement>().SetVelocity(moveVector);


        if (IsPressingJump()) {
            pv.anim.SetTrigger("DoubleJump");
        }
    }

    public void DoubleJump_Enter() {
        pm.DoubleJump();
    }

    public void DoubleJump_Update() {
        MoveHorizontally();
        float moveX = 0;
        if (isGoingRight) moveX = 1;
        if (isGoingLeft) moveX = -1;
        moveVector = new Vector3(moveX, 0, 0).normalized;
        GetComponent<IMovement>().SetVelocity(moveVector);
    }



    void MoveFunction() {
        MoveHorizontally();
        float moveX = 0;
        if (isGoingRight) moveX = 1;
        if (isGoingLeft) moveX = -1;
        moveVector = new Vector3(moveX, 0, 0).normalized;
        GetComponent<IMovement>().SetVelocity(moveVector);
        
        pv.anim.SetFloat("Speed", Mathf.Abs(moveVector.x));
    }

    void AttackInputFunction() {
        if (IsPressingLightAttack()) {
            pv.anim.Play("LightAttack01");
        }
        if (IsPressingHeavyAttack()) {
            pv.anim.Play("HeavyAttack01");
        }
    }

    void DashInputFunction() {
        if (IsPressingDash())
        {
            pv.anim.SetTrigger("Dash");
        }
    }
    void LaunchRangedPowerFunction() {
        if (IsPressingRangedPower())
        {
            GameObject obj = Instantiate(rangedPowerObject, pae.attackPoint.position, Quaternion.identity);
            obj.GetComponent<PlayerProjectile>().SetPlayerController(this);
            obj.GetComponent<Rigidbody>().velocity = transform.right * rangedPowerForce;
        }
    }

    void AttachToRopeFunction() {
        if (rope) {
            MoveVertically();
            if (isGoingUp || isGoingDown) {
                isClimbing = true;
                pv.anim.SetBool("isClimbing", isClimbing);
            }
        }
    }

    public void Rope_Enter() {
        //pv.rb.useGravity = false;
        //pv.rb.isKinematic = true;

    }

    public void Rope_Update() {
        if (rope)
        {
            MoveHorizontally();
            MoveVertically();
            float moveX = 0;
            if (isGoingRight) moveX = 1;
            if (isGoingLeft) moveX = -1;

            float moveY = 0;
            if (isGoingUp) moveY = 1;
            if (isGoingDown) moveY = -1;
            moveVector = new Vector3(moveX, moveY, 0).normalized;
            GetComponent<IMovement>().SetVelocity(moveVector);

            pv.selfCollider.isTrigger = (transform.position.y > rope.localPosition.y) ? true : false;
        }else if (isGoingLeft || isGoingRight || !rope)
        {
            pv.selfCollider.isTrigger = false;
            isClimbing = false;
            pv.anim.SetBool("isClimbing", isClimbing);
        }




    }

    public void Rope_Exit() {
        //pv.rb.useGravity = true;
        //pv.rb.isKinematic = false;
    }

    public void MoveToRope_Update() {
        transform.position = Vector3.Lerp(transform.position, ropeDestination, 0.1f);
        isClimbing = Mathf.Abs(Vector3.Distance(transform.position, ropeDestination)) < 1f;
        pv.anim.SetBool("isClimbing", isClimbing);
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
