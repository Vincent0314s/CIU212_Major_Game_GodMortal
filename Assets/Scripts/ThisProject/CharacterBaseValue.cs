using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackBasicValue
{
    public AttackType type;
    public float damage;
    public AttackBasicValue(AttackType _type, float _damage)
    {
        type = _type;
        damage = _damage;
    }
}

public class CharacterBaseValue : MonoBehaviour
{
    public bool isReadyToLoadComponment;

    [Header("HP")]
    public float maxHP = 100f;
    private float currentHP;

    [Header("Stamina")]
    public float maxStamina = 100f;
    private float currentStamina;

    [Header("Attack")]
    [ArrayElementTitle("type")]
    public List<AttackBasicValue> attackSetting = new List<AttackBasicValue>() {
        new AttackBasicValue(AttackType.Light, 30),
        new AttackBasicValue(AttackType.Heavy,45)
    };

    public bool isLightAttacking { get; private set; }
    public bool isHeavyAttacking { get; private set; }

    private float damage;

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }


    void Start() {
        currentHP = maxHP;
        currentStamina = maxStamina;
        if (isReadyToLoadComponment) {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
        }
    }

    public float GetDamageFromAttackType(AttackType _type)
    {
        for (int i = 0; i < attackSetting.Count; i++)
        {
            if (attackSetting[i].type == _type)
            {
                damage = attackSetting[i].damage;
            }
        }
        return damage;
    }

    public float GetHealthPercentage() {
        return currentHP / maxHP;
    }

    public float GetStaminaPercentage() {
        return currentStamina / maxStamina;
    }


    //isAttacking
    public bool IsLightAttacking(bool condition) {
        isLightAttacking = condition;
        return isLightAttacking;
    }

    public bool IsLightAttacking(KeyCode condition)
    {
        isLightAttacking = Input.GetKeyDown(condition);
        return isLightAttacking;
    }

    public bool IsHeavyAttacking(bool condition)
    {
        isHeavyAttacking = condition;
        return isHeavyAttacking;
    }

    public bool IsHeavyAttacking(KeyCode condition)
    {
        isHeavyAttacking = Input.GetKeyDown(condition);
        return isHeavyAttacking;
    }
}
