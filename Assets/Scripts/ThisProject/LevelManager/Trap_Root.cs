using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Root : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            if (!transform.GetChild(0).gameObject.activeInHierarchy) { 
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }


}
