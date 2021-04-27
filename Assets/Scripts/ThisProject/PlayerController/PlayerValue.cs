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

    [Space]
    [Header("UI")]
    public Image UI_HPBar;
    public Image UI_StBar;


    public override void InitPlayerState()
    {
        base.InitPlayerState();
        currentStamina = maxStamina;
        UpdatePlayerHpBar();
    }
    public float GetStaminaPercentage()
    {
        return currentStamina / maxStamina;
    }

    public override void AddHP(float _amount)
    {
        base.AddHP(_amount);
        UpdatePlayerHpBar();
    }

    public override void GetHurt(float damage)
    {
        base.GetHurt(damage);
        UpdatePlayerHpBar();
    }
    public override void GetHurt(float damage, Action deadEvent)
    {
        base.GetHurt(damage, deadEvent);
        UpdatePlayerHpBar();
    }

    public void UpdatePlayerHpBar()
    {
        if (UI_HPBar)
        {
            UI_HPBar.fillAmount = GetHealthPercentage();
        }
    }

    public void UpdatePlayerStBar() {
        if (UI_StBar)
        {
            UI_StBar.fillAmount = GetStaminaPercentage();
        }
    }
}
