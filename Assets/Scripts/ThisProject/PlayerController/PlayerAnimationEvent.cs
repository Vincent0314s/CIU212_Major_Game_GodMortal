using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerAnimationEvent : MonoBehaviour
{
    PlayerController pc;
    public float forwardForceValue = 5f;

    public Transform attackPoint;
    public float lightAttackRadius = 0.25f;
    public Color lightAttackRangeColor = Color.white;
    public LayerMask enemyMask;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    public void ForwardForce() {
        //pc.cbv.rb.AddForce(pc.transform.right * forwardForceValue);
        pc.cbv.rb.velocity = pc.transform.right * forwardForceValue;
    }

    public void LightAttackRange() {
        Collider[] colls = Physics.OverlapSphere(attackPoint.position, lightAttackRadius, enemyMask);
        if (colls.Length > 0) {
            for (int i = 0; i < colls.Length; i++)
            {
                colls[i].GetComponent<CharacterBaseValue>().GetHurt(pc.cbv.GetDamageAmountFromAttackType(AttackType.Light),()=> { colls[i].GetComponent<EnemyController>().DisableCollision(); });
            }
        }
    }

 

    private void OnDrawGizmosSelected()
    {
        if (attackPoint) {
            Handles.color = lightAttackRangeColor;
            Handles.DrawWireDisc(attackPoint.position, transform.parent.forward, lightAttackRadius);
            GUI.color = Color.white;
            Handles.Label(attackPoint.position, lightAttackRadius.ToString("f1"));
        }
    }
}
