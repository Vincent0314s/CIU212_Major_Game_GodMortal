using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent_Ranged : MonoBehaviour
{

    public GameObject arrow;
    public Transform attackPoint;
    public float shootForce = 2f;
    EnemyController ec;

    private void Start()
    {
        ec = GetComponentInParent<EnemyController>();    
    }
    public void LightAttackRange() {
        Vector3 direction = ec.player.position - attackPoint.position + new Vector3(0,1.35f,0);
        attackPoint.transform.right = direction;
        GameObject arrowIns = Instantiate(arrow, attackPoint.position, attackPoint.rotation);
        arrowIns.GetComponent<Arrow>().SetEnemyController(ec);
        arrowIns.GetComponent<Rigidbody>().velocity = attackPoint.transform.right * shootForce;
    }
}
