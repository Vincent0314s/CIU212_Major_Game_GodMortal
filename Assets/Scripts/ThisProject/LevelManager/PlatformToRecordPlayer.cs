using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToRecordPlayer : MonoBehaviour
{
    public bool isStaying;
    public float stayTimeToRecord = 1.5f;
    private float currentTimer;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (!isStaying) {
                if (currentTimer < stayTimeToRecord)
                {
                    currentTimer += Time.deltaTime;
                }
                else
                {
                    isStaying = true;
                    currentTimer = 0;
                }
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isStaying = false;
        }
    }
}
