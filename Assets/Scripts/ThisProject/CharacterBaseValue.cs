﻿using System.Collections;
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
    public float maxHP = 100f;
    public float currentHP { get; private set; }

    [Header("Stamina")]
    public float maxStamina = 100f;
    private float currentStamina;

    [Header("Attack")]
    [ArrayElementTitle("type")]
    public List<AttackBasicValue> attackSetting = new List<AttackBasicValue>() {
        new AttackBasicValue(AttackType.Light, 30),
        new AttackBasicValue(AttackType.Heavy,45)
    };

    [Space]
    [Header("UI")]
    public Image UI_HPBar;
    public bool isLightAttacking { get; private set; }
    public bool isHeavyAttacking { get; private set; }
    public bool isDead { get; private set; }

    private float damage;

    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }


    void Start() {
        currentHP = maxHP;
        currentStamina = maxStamina;
        UpdatePlayerHpBar();
        if (isReadyToLoadComponment) {
            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
        }
    }

    public float GetDamageAmountFromAttackType(AttackType _type)
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

    public void AddHP(float _amount) {
        currentHP += _amount;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        UpdatePlayerHpBar();
    }

    public void GetHurt(float damage)
    {
        currentHP -= damage;
        if (currentHP > 0)
        {
            anim.Play("Hurt",0,0);
        }
        else {
            anim.Play("Dead");
            isDead = true;
        }
        UpdatePlayerHpBar();
    }

    public void GetHurt(float damage, Action deadEvent)
    {
        currentHP -= damage;
        if (currentHP > 0)
        {
            anim.Play("Hurt",0,0);
        }
        else
        {
            deadEvent?.Invoke();
            anim.Play("Dead");
            isDead = true;
        }
        UpdatePlayerHpBar();
    }

    private void UpdatePlayerHpBar() {
        if (UI_HPBar)
        {
            UI_HPBar.fillAmount = GetHealthPercentage();
        }
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
