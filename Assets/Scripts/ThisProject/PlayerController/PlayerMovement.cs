using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    public int doubleJumpTimes = 1;
    private int currentdoubleJumpTimes;
    PlayerController pc;

    public override void Start()
    {
        base.Start();
        pc = GetComponent<PlayerController>();
        currentdoubleJumpTimes = doubleJumpTimes;
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
}
