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
    public LayerMask destructiveObjectMask;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        transform.localPosition = new Vector3(0, 0.266f, 0);
    }

    public void ForwardForce() {
        //pc.cbv.rb.AddForce(pc.transform.right * forwardForceValue);
        pc.pv.rb.velocity = pc.transform.right * forwardForceValue;
        //pc.pv.rb.AddForce(pc.transform.right*forwardForceValue,ForceMode.VelocityChange);
    }

    public void Jump() {
        pc.Jump_Enter();
    }

    public void LightAttackRange() {
        //Enemy
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, lightAttackRadius, enemyMask);
        if (enemies.Length > 0) {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<CharacterBaseValue>().GetHurt(pc.pv.GetDamageAmountFromAttackType(AttackType.Light),true);
            }
        }
        //Destructive object
        Collider[] DestructiveObjects = Physics.OverlapSphere(attackPoint.position, lightAttackRadius, destructiveObjectMask);
        if (DestructiveObjects.Length > 0)
        {
            for (int i = 0; i < DestructiveObjects.Length; i++)
            {
                DestructiveObjects[i].GetComponent<DestructiveObject>().BeingBroken();
            }
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackPoint) {
            Handles.color = lightAttackRangeColor;
            Handles.DrawWireDisc(attackPoint.position, transform.parent.forward, lightAttackRadius);
            GUI.color = Color.white;
            Handles.Label(attackPoint.position, lightAttackRadius.ToString("f1"));
        }
    }
#endif
}
