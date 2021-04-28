using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiveObject : MonoBehaviour
{
    public GameObject itemPrefab;

    Rigidbody[] rb;

    private void Start()
    {
        rb = transform.GetChild(0).GetComponentsInChildren<Rigidbody>();
    }

    public void BeingBroken() {
        GetComponent<Collider>().isTrigger = true;
        Instantiate(itemPrefab,transform.position,Quaternion.identity);
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].useGravity = true;
            rb[i].isKinematic = false;
        }
        Destroy(this.gameObject, 5f);
    }

}
