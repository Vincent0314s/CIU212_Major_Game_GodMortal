using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 colliderOffset = new Vector3(0,1.8f,0);
    public Vector3 colliderSize = new Vector3(0.01f,1.5f,1);
    public Color colliderColor = Color.cyan;

    private GameObject leftCollider;
    private GameObject rightCollider;


    public void GenerateConfineedCollider() {
        if (AreCollidersNull()) {
            leftCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rightCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);

            leftCollider.tag = "PlatformCollider";
            rightCollider.tag = "PlatformCollider";

            leftCollider.transform.SetParent(transform, true);
            rightCollider.transform.SetParent(transform, true);

            leftCollider.transform.localScale = colliderSize;
            rightCollider.transform.localScale = colliderSize;

            float scaleX = transform.localScale.x;

            leftCollider.transform.localPosition = new Vector3(-colliderOffset.x, colliderOffset.y, colliderOffset.z);
            rightCollider.transform.localPosition = new Vector3(colliderOffset.x, colliderOffset.y, colliderOffset.z);


            leftCollider.GetComponent<BoxCollider>().isTrigger = true;
            rightCollider.GetComponent<BoxCollider>().isTrigger = true;

            DestroyImmediate(leftCollider.GetComponent<MeshRenderer>());
            DestroyImmediate(rightCollider.GetComponent<MeshRenderer>());
        }
    }

    public void UpdateData() {
        leftCollider.transform.localScale = colliderSize;
        rightCollider.transform.localScale = colliderSize;

        float scaleX = transform.localScale.x;

        leftCollider.transform.localPosition = new Vector3(-colliderOffset.x, colliderOffset.y, colliderOffset.z);
        rightCollider.transform.localPosition = new Vector3(colliderOffset.x, colliderOffset.y, colliderOffset.z);
    }

    public bool AreCollidersNull() {
        if (leftCollider && rightCollider)
        {
            return false;
        }
        else if(!leftCollider && !rightCollider){
            return true;
        }
        return false;
    }

    public void ClearBorder() {
        if (!AreCollidersNull()) {
            DestroyImmediate(leftCollider);
            leftCollider = null;
            DestroyImmediate(rightCollider);
            rightCollider = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = colliderColor;
        if (leftCollider) { 
            Gizmos.DrawCube(leftCollider.transform.position,leftCollider.transform.lossyScale);
        }
        if (rightCollider)
        {
            Gizmos.DrawCube(rightCollider.transform.position, rightCollider.transform.lossyScale);
        }
    }

}
