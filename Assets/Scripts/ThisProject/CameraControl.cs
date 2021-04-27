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

    public Vector2 limitedHeight = new Vector2(1.5f,6.5f);

    private void Awake()
    {
        AssignPlayerToFocus();
    }

    void LateUpdate()
    {
        float posY = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position,target.position + offset,ref velocity, speedValue);
        posY = Mathf.Clamp(transform.position.y, limitedHeight.x,limitedHeight.y);

        transform.position = new Vector3(transform.position.x,posY,transform.position.z);

        //transform.position = Vector3.Slerp(transform.position,target.position + offset, followSpeed);
    }

    public void AssignPlayerToFocus() {
        target = GameAssetManager.i.currentPlayer.transform;
    }

}
