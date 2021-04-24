using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody rb;
    private EnemyController ec;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetEnemyController(EnemyController _ec) {
        ec = _ec;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            collision.transform.GetComponent<CharacterBaseValue>().GetHurt(ec.cbv.GetDamageAmountFromAttackType(AttackType.Light));
        }
        Destroy(this.gameObject);
    }
}
