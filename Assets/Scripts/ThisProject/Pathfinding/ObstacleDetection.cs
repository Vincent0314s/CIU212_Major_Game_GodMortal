using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    public bool isBeingBlocked;
    public float timeToBack = 1f;
    private float currentBackTime;
    //[SerializeField]
    //private SkinnedMeshRenderer meshRenderer;

    private void Start()
    {
        //meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        if (isBeingBlocked)
        {
            if (currentBackTime < timeToBack)
            {
                currentBackTime += Time.deltaTime;
            }
            else
            {
                currentBackTime = 0;
                isBeingBlocked = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            //meshRenderer.enabled = false;
            isBeingBlocked = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (!isBeingBlocked)
            {
                isBeingBlocked = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            //meshRenderer.enabled = true;
        }
    }
}
