using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float currentMoveSpeed = 10f;

    private float turnSmoothVelocity;
    public float turnSmoothValue = 0.35f;

    private bool isTurningRight;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1,0,0) * currentMoveSpeed * Time.deltaTime;
            isTurningRight = true;
          

        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * currentMoveSpeed * Time.deltaTime;
            isTurningRight = false;
        }
        if (isTurningRight)
        {
            if (transform.eulerAngles.y != 0)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref turnSmoothVelocity, turnSmoothValue);
                transform.rotation = Quaternion.Euler(0, angle, 0f);
            }
        }
        else {
            if (transform.eulerAngles.y != 180)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 180, ref turnSmoothVelocity, turnSmoothValue);
                transform.rotation = Quaternion.Euler(0, angle, 0f);
            }
        }
    }
}
