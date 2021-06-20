using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
    public HealthManager healthSetting;

    [Header("Attack")]
    [ArrayElementTitle("type")]
    public List<AttackBasicValue> attackSetting = new List<AttackBasicValue>() {
        new AttackBasicValue(AttackType.Light, 30),
        new AttackBasicValue(AttackType.Heavy,45)
    };

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }


    void Start() {
        Initialzation();
        if (isReadyToLoadComponment) {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
        }
    }

    public virtual void Initialzation() {
        healthSetting.Initialization();
        healthSetting.OnDeadEvent = () =>
        {
            Dead();
        };
    }

    public float GetDamageAmountFromAttackType(AttackType _type)
    {
        for (int i = 0; i < attackSetting.Count; i++)
        {
            if (attackSetting[i].type == _type)
            {
                return attackSetting[i].damage;
            }
        }
        return 0;
    }

   
    public virtual void AddHP(float _amount) {
        healthSetting.GetHeal(_amount);
    }

    public virtual void GetHurt(float _damage, bool _canPlayAnimation)
    {
        if (_canPlayAnimation)
        {
            healthSetting.GetHurt(_damage, () => anim.Play("Hurt", 0, 0), () => anim.Play("Dead"));
        }
        else {
            healthSetting.GetHurt(_damage, null, () => anim.Play("Dead"));
        }
    }

    public virtual void Dead() { 

    }
   
}
