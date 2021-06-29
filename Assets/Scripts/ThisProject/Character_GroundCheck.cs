using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_GroundCheck : MonoBehaviour
{
    [Space]
    [Header("GroundCheck")]
    public float detectGroundDistance = 1.5f;
    public float groundDetectedOffsetX = 0.36f;
    public float groundDetectedOffsetY;
    public bool isOnGround { get; private set; }

    public bool isOnSlope { get; private set; }
    public float slopeDistance;
    public RaycastHit slopeHit;

    public LayerMask groundLayer;
    protected CharacterBaseValue cbv;

    void Awake()
    {
        cbv = GetComponent<CharacterBaseValue>();
    }

    public void OnGround(bool _b)
    {
        if (_b)
        {
            isOnGround = Physics.Raycast(transform.position + new Vector3(groundDetectedOffsetX, groundDetectedOffsetY, 0), Vector3.down, detectGroundDistance, groundLayer)
                    || Physics.Raycast(transform.position - new Vector3(groundDetectedOffsetX, -groundDetectedOffsetY, 0), Vector3.down, detectGroundDistance, groundLayer);
            //if (Physics.Raycast(transform.position,Vector3.down, out slopeHit, slopeDistance, groundLayer)) {
            //    //cbv.rb.AddForce((transform.position - slopeHit.point).normalized * 9.8f);
            //}

        }
        else
        {
            isOnGround = false;
        }

    }

    public void OnSlope(bool _b)
    {
        if (_b)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, slopeDistance, groundLayer))
            {
                if (slopeHit.normal != Vector3.up)
                {
                    isOnSlope = true;
                }
                else
                {
                    isOnSlope = false;
                }
            }
        }
        else
        {
            isOnSlope = false;
        }
    }

    private void OnDrawGizmos()
    {
        //Display a line for jumping detection
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(groundDetectedOffsetX, groundDetectedOffsetY, 0), transform.position + new Vector3(groundDetectedOffsetX, groundDetectedOffsetY, 0) + Vector3.down * detectGroundDistance);
        Gizmos.DrawLine(transform.position - new Vector3(groundDetectedOffsetX, -groundDetectedOffsetY, 0), transform.position - new Vector3(groundDetectedOffsetX, -groundDetectedOffsetY, 0) + Vector3.down * detectGroundDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector3.down * slopeDistance);
    }
}
