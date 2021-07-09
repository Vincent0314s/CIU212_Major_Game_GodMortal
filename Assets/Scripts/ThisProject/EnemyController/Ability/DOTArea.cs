using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTArea : MonoBehaviour
{
    public float dps;
    public float duration;

    private void OnEnable()
    {
        //Invoke("DestroyArea",duration);
        Destroy(this.gameObject,duration);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            other.GetComponent<PlayerValue>().GetHurt(dps,false);
        }
    }

    void DestroyArea() {
        Destroy(this.gameObject);
    }
}
