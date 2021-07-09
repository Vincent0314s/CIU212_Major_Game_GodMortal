using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiveObject : MonoBehaviour
{
    public PercentageManager dropPercentage;

    Rigidbody[] rb;

    private void Start()
    {
        rb = transform.GetChild(0).GetComponentsInChildren<Rigidbody>();
        dropPercentage.Initialization();
    }

    public void BeingBroken() {
        //GetComponent<Collider>().isTrigger = true;
        GetComponent<Collider>().enabled = false;
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].useGravity = true;
            rb[i].isKinematic = false;
        }
        Destroy(this.gameObject, 5f);

        switch (dropPercentage.GetCertainPercentageFromList()) {
            case "None":
                return;
            case "HP_Potion":
                Instantiate(GameAssetManager.i.HP_Potion, transform.position, Quaternion.identity);
                break;
        }
     
    }

}
