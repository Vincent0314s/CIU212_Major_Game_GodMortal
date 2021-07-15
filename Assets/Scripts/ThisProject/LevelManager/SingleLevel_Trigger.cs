using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevel_Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<SingleLevel>().CallTrigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        gameObject.GetComponentInParent<SingleLevel>().CallTriggerStay(other);
    }
}
