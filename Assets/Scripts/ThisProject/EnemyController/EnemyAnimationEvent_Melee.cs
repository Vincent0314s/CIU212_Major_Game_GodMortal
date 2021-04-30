using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyAnimationEvent_Melee : MonoBehaviour
{
    CharacterBaseValue cbv;
    public Transform attackPoint;
    public float lightAttackRadius = 0.25f;
    public Color lightAttackRangeColor = Color.white;
    public LayerMask playerMask;

    private void Start()
    {
        cbv = GetComponentInParent<CharacterBaseValue>();
    }

    public void LightAttackRange()
    {
        Collider[] colls = Physics.OverlapSphere(attackPoint.position, lightAttackRadius, playerMask);
        if (colls.Length > 0)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                colls[i].GetComponent<CharacterBaseValue>().GetHurt(cbv.GetDamageAmountFromAttackType(AttackType.Light));
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackPoint)
        {
            Handles.color = lightAttackRangeColor;
            Handles.DrawWireDisc(attackPoint.position, transform.parent.forward, lightAttackRadius);
            GUI.color = lightAttackRangeColor;
            Handles.Label(attackPoint.position, lightAttackRadius.ToString("f1"));
        }
    }
#endif
}
