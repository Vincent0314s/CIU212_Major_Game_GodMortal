using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody rb;
    private bool hitSomething;
    private EnemyController ec;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    public void SetEnemyController(EnemyController _ec) {
        ec = _ec;
    }
    void Update()
    {
        //if (!hitSomething)
        //{
        //    transform.rotation = Quaternion.LookRotation(rb.velocity);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSomething = true;
        if (collision.transform.tag == "Player") {
            collision.transform.GetComponent<CharacterBaseValue>().GetHurt(ec.cbv.GetDamageAmountFromAttackType(AttackType.Light));
        }
        Destroy(this.gameObject);
    }
}
