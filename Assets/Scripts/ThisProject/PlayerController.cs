using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(3,20)]
    public float currentMoveSpeed = 10f;

    private float turnSmoothVelocity;
    [Range(0,1)]
    public float turnSmoothValue = 0.35f;

    private bool isTurningRight;
    private bool isJumping;
    private bool isOnTheAir;

    public float jumpForce = 5f;
    public float fallValue = 2.5f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        MovementInput();
        
        if (Input.GetKeyDown(KeyCode.Space)) isJumping = true;

    }

    void MovementInput() {
        float moveX = 0;
        if (Input.GetKey(KeyCode.D)) moveX = 1;
        if (Input.GetKey(KeyCode.A)) moveX = -1;
        Vector3 moveVector = new Vector3(moveX, 0, 0).normalized;
        GetComponent<IMovement>().SetVelocity(moveVector);
    }


    
    //void InputControl() {
    //    isTurningRight = Input.GetKey(KeyCode.D);
    //    isJumping = Input.GetKeyDown(KeyCode.Space);
    //}

    //void MovementControl() {
    //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
    //    {
    //        if (isTurningRight)
    //        {
    //            transform.position += new Vector3(1, 0, 0) * currentMoveSpeed * Time.deltaTime;
    //            //if (transform.eulerAngles.y != 0)
    //            //{
    //            //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref turnSmoothVelocity, turnSmoothValue);
    //            //    transform.rotation = Quaternion.Euler(0, angle, 0f);
    //            //}
    //            transform.eulerAngles = new Vector3(0, 0, 0);
    //        }
    //        else
    //        {
    //            transform.position += new Vector3(-1, 0, 0) * currentMoveSpeed * Time.deltaTime;
    //            //if (transform.eulerAngles.y != 180)
    //            //{
    //            //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 180, ref turnSmoothVelocity, turnSmoothValue);
    //            //    transform.rotation = Quaternion.Euler(0, angle, 0f);
    //            //}
    //            transform.eulerAngles = new Vector3(0, 180, 0);
    //        }
    //    }
    //    JumpControl();
    //}

    //void JumpControl() {
    //    if (isJumping)
    //    {
    //        rb.velocity = Vector3.up * jumpForce;
            
    //    }

    //    if (rb.velocity.y < 0)
    //    {
    //        rb.velocity += Vector3.up * Physics.gravity.y * (fallValue - 1) * Time.deltaTime;
    //    }
    //}
}
