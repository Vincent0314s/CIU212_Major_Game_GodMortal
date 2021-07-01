using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    public bool isBeingBlocked;
    public float timeToBack = 1f;
    private float currentBackTime;

    private void Update()
    {
        if (isBeingBlocked) {
            if (currentBackTime < timeToBack)
            {
                currentBackTime += Time.deltaTime;
            }
            else {
                currentBackTime = 0;
                isBeingBlocked = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isBeingBlocked = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (!isBeingBlocked) { 
                isBeingBlocked = true;
            }
        }
    }
}
