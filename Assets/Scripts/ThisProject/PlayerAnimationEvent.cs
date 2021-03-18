using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    PlayerController pc;
    public float forwardForceValue = 5f;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    public void ForwardForce() {
        //pc.cbv.rb.AddForce(pc.transform.right * forwardForceValue);
        pc.cbv.rb.velocity = pc.transform.right * forwardForceValue;
    }
}
