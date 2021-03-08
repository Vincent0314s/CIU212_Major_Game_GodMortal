using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterMovement cm;
    private bool isGoingRight;
    private bool isGoingLeft;
    private bool isDash;

    public float dashForce = 5;

    void Start()
    {
        cm = GetComponent<CharacterMovement>();
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
        GetComponent<IMovement>().SetVelocity(moveVector);

        //if (isDash) {
        //    if (isGoingRight) {
        //        transform.position = new Vector3(transform.position.x+dashForce,transform.position.y,transform.position.z);
        //    }
        //}

        cm.JumpState(Input.GetKeyDown(KeyCode.Space));

       
    }
}
