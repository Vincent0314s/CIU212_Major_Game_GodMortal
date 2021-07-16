using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent_LifeBoss : MonoBehaviour
{
    EnemyController_LifeBoss elb;
    public GameObject vineEffect;
    void Start()
    {
        elb = GetComponentInParent<EnemyController_LifeBoss>();
    }

    public void TeleporatToPoint() {
        int randomPos = Random.Range(0,elb.teleportPoints.Length);
        transform.parent.position = elb.teleportPoints[randomPos].position;
    }

    public void ActiveVine() {
        vineEffect.SetActive(true);
    }
}
