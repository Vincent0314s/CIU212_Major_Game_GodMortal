using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public Vector3 colliderSize = new Vector3(0.08f,2,1);
    public void GenerateConfineedCollider() {
        GameObject leftCollider = new GameObject("LeftCollider");
        GameObject rightCollider = new GameObject("RightCollider");

        leftCollider.tag = "PlatformCollider";
        rightCollider.tag = "PlatformCollider";
        leftCollider.transform.SetParent(transform,false);
        rightCollider.transform.SetParent(transform,false);

        leftCollider.AddComponent<BoxCollider>();
        rightCollider.AddComponent<BoxCollider>();
        leftCollider.GetComponent<BoxCollider>().isTrigger = true;
        rightCollider.GetComponent<BoxCollider>().isTrigger = true;
        leftCollider.GetComponent<BoxCollider>().size = colliderSize;
        rightCollider.GetComponent<BoxCollider>().size = colliderSize;

    }
}
