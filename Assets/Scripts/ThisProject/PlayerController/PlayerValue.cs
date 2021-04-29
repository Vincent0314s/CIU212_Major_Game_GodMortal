using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StaminaValue {
    
    public PlayerActionType actionType;
    public float amount;
}

public class PlayerValue : CharacterBaseValue
{
    private static PlayerValue _i;
    public static PlayerValue i {
        get {
            if (_i == null) {
                _i = GameObject.FindObjectOfType<PlayerValue>();
            }
            return _i;
        }
    }


    [Header("Stamina")]
    public float maxStamina = 100f;
    [HideInInspector]
    public float currentStamina;
    public float recoverSpeed;

    [ArrayElementTitle("actionType")]
    public List<StaminaValue> staminaList;

    public float maxShield = 50;
    private float currentShield;

    [Space]
    [Header("UI")]
    public Image UI_HPBar;
    public Image UI_StaminaBar;
    public Image UI_ShieldBar;


    public override void InitPlayerState()
    {
        base.InitPlayerState();
        currentShield = maxShield;
        currentStamina = maxStamina;
        UpdatePlayerHpBar();
        UpdatePlayerShieldBar();
    }
    public float GetStaminaPercentage()
    {
        return currentStamina / maxStamina;
    }

    public float GetShieldPercentage() {
        return currentShield / maxShield;
    }

    public override void AddHP(float _amount)
    {
        base.AddHP(_amount);
        UpdatePlayerHpBar();
    }

    //public override void GetHurt(float damage)
    //{
    //    base.GetHurt(damage);
    //    UpdatePlayerHpBar();
    //}

    public override void GetHurt(float damage, Action deadEvent)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
        }
        else {
            currentHP -= damage;
            if (currentHP > 0)
            {
                anim.Play("Hurt", 0, 0);
            }
            else
            {
                deadEvent?.Invoke();
                anim.Play("Dead");
                isDead = true;
            }
        }
        UpdatePlayerShieldBar();
        UpdatePlayerHpBar();
    }

    public void UpdatePlayerHpBar()
    {
        if (UI_HPBar)
        {
            UI_HPBar.fillAmount = GetHealthPercentage();
        }
    }

    public void UpdatePlayerShieldBar() {
        if (UI_ShieldBar) {
            UI_ShieldBar.fillAmount = GetShieldPercentage();
        }
    }

    public void UpdatePlayerStBar() {
        if (UI_StaminaBar)
        {
            UI_StaminaBar.fillAmount = GetStaminaPercentage();
        }
    }
}
