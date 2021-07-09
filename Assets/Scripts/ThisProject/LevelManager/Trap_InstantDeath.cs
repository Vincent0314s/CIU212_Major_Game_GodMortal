using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_InstantDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            other.GetComponent<PlayerValue>().GetHurt_IgonoreShield(10000,true);
        }
    }
}
