using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            other.GetComponent<PlayerController>().rope = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().rope = null;
        }
    }
}
