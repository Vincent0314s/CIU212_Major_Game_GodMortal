using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private Transform target;
    public Vector3 offset;
    [Range(0,1)]
    public float speedValue;
    private Vector3 velocity;

    public float distanceBetweenBoss = 20f;
    public Vector2 limitedZ = new Vector2(-15f,-30f);
    public Vector2 limitedY = new Vector2(-50,30f);

    public Transform boss;

    private float oriZ;

    private void Start()
    {
        AssignPlayerToFocus();
        oriZ = offset.z;

    }


    void LateUpdate()
    {
        float posY = transform.position.y;
        //if (target.GetComponent<PlayerController>().isInBossRoom)
        //{
        //    float dis = Vector3.Distance(target.position, boss.position);
        //    if (dis > distanceBetweenBoss)
        //    {
        //        offset.z -= (dis - distanceBetweenBoss);
        //    }
        //    else
        //    {
        //        offset.z = oriZ;
        //    }
        //}
       
        transform.position = Vector3.SmoothDamp(transform.position,target.position + offset,ref velocity, speedValue);
        posY = Mathf.Clamp(transform.position.y, limitedY.x,limitedY.y);
        //offset.z = Mathf.Clamp(offset.z, limitedZ.x,limitedZ.y);

        transform.position = new Vector3(transform.position.x,posY, transform.position.z);

        //transform.position = Vector3.Slerp(transform.position,target.position + offset, followSpeed);
    }

    public void AssignPlayerToFocus() {
        target = GameAssetManager.i.currentPlayer.transform;
    }


}
