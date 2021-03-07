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

    [Header("Attack")]
    [ArrayElementTitle("type")]
    public List<AttackBasicValue> attackSetting = new List<AttackBasicValue>() {
        new AttackBasicValue(AttackType.Light, 30),
        new AttackBasicValue(AttackType.Heavy,45)
    };

    private float damage;

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }


    void Start() {
        currentHP = maxHP;
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
}
