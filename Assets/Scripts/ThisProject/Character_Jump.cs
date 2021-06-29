using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Jump : MonoBehaviour
{
    [Space]
    [Header("Jump")]
    public float jumpForce = 5f;
    public float fallValue = 2.5f;
    [Range(0, 5)]
    [Tooltip("How long can player stay in the air, 0 is longest")]
    public float heightToFall = 2f;
    public bool defaultJumpCondition;

    public int extraJumpCounts = 1;
    public int currentJumpTimes { get; private set; }

    protected CharacterBaseValue cbv;

    void Awake()
    {
        cbv = GetComponent<CharacterBaseValue>();
    }

    private void Start()
    {
        ResetJumpCounts();
    }

    private void FixedUpdate()
    {
        if (defaultJumpCondition)
            UpdateFallingGravity();
    }

    public void Jump()
    {
        //if (isOnGround)
        //{
           
        //}
        cbv.rb.velocity = new Vector3(cbv.rb.velocity.x, 0, cbv.rb.velocity.z);
        cbv.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void UpdateFallingGravity() {
        if (cbv.rb.velocity.y < heightToFall)
        {
            cbv.rb.velocity += Vector3.up * Physics.gravity.y * (fallValue - 1) * Time.fixedDeltaTime;
        }
    }

    public void ResetJumpCounts()
    {
        currentJumpTimes = extraJumpCounts;
    }

    public void SetJumpCounts(int _t) {
        currentJumpTimes = _t;
    }

}
