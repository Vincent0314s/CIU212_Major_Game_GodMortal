using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    [Range(0,1)]
    public float followSpeed;


    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position,target.position + offset, followSpeed);
    }

}
