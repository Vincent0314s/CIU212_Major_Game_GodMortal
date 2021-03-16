using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    CharacterMovement cm;
    CharacterBaseValue cbv;
    private bool isGoingRight;
    private bool isGoingLeft;
    private bool isDash;

    public float dashForce = 5;
    public float dashTimer= 0.5f;
    private float dashStartTimer;

    void Start()
    {
        cm = GetComponent<CharacterMovement>();
        cbv = GetComponent<CharacterBaseValue>();
        dashStartTimer = dashTimer;
    }

    void Update()
    {
        InputSetting();
        MovementInput();
    }

    void InputSetting() {
        isGoingRight = Input.GetKey(KeyCode.D);
        isGoingLeft = Input.GetKey(KeyCode.A);
        isDash = Input.GetKeyDown(KeyCode.LeftShift);
    }

    void MovementInput() {
        float moveX = 0;
        if (isGoingRight) moveX = 1;
        if (isGoingLeft) moveX = -1;
        Vector3 moveVector = new Vector3(moveX, 0, 0).normalized;
        if (dashTimer >=0.5f) {
            GetComponent<IMovement>().SetVelocity(moveVector);
        }

        cbv.anim.SetFloat("Speed",Mathf.Abs(moveVector.x));
        cbv.anim.SetBool("isDash",isDash);
        if (isDash)
        {
            DashForward();
            Debug.Log("Dash!");
        }

        cm.JumpState(Input.GetKeyDown(KeyCode.Space));

       
    }

    void DashForward() {
        cbv.rb.velocity = transform.right * dashForce;
    }
}
