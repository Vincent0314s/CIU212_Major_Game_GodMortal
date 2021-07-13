using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerAnimationEvent : MonoBehaviour
{
    PlayerController pc;
    public float forwardForceValue = 5f;

    public Transform attackPoint;
    [Header("LightAttack")]
    public float lightAttackRadius = 1f;
    public Color lightAttackRangeColor = Color.white;

    [Header("HeavyAttack")]
    public float heavyAttackRaduis = 2f;
    public Color heavyAttackRangeColor = Color.blue;

    public GameObject childLetter;

    public LayerMask enemyMask;
    public LayerMask destructiveObjectMask;

    private void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }


    public void ForwardForce() {
        //pc.cbv.rb.AddForce(pc.transform.right * forwardForceValue);
        pc.pv.rb.velocity = pc.transform.right * forwardForceValue;
        //pc.pv.rb.AddForce(pc.transform.right*forwardForceValue,ForceMode.VelocityChange);
    }

    public void Jump() {
        pc.Jump_Enter();
    }

    public void LaunchRangedPower() {
        pc.LaunchRangedPower();
    }

    public void PlayerRangePowerSFX() {
        SoundManager.PlaySound(SoundEffect.FireBallCasting);
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

    public void HeavyAttackRange() {
        //Enemy
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, heavyAttackRaduis, enemyMask);
        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<CharacterBaseValue>().GetHurt(pc.pv.GetDamageAmountFromAttackType(AttackType.Heavy), true);
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

    public void DrinkingPotion(int _potionIndex) {
        switch (_potionIndex) {
            //HP
            case 0:
                ItemManager.i.RemoveItems(Items.HealthPotion);
                pc.pv.AddHP(ItemManager.i.healthPotionHealAmount);
                break;
            //Stamina
            case 1:
                //ItemManager.i.RemoveItems(Items.StaminaPotion);
                break;
        }
    }

    public void ActivateLetter(int _i) {
        if (_i == 0)
        {
            childLetter.SetActive(false);
        }
        else {
            childLetter.SetActive(true);
        }
    }

    public void OpenLetter() {
        ItemManager.i.OpenLetter();
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (attackPoint) {
            Handles.color = lightAttackRangeColor;
            Handles.DrawWireDisc(attackPoint.position, transform.parent.forward, lightAttackRadius);
            //GUI.color = Color.white;
            //Handles.Label(attackPoint.position, lightAttackRadius.ToString("f1"));

            Handles.color = heavyAttackRangeColor;
            Handles.DrawWireDisc(attackPoint.position, transform.parent.forward, heavyAttackRaduis);
        }
    }
#endif
}
