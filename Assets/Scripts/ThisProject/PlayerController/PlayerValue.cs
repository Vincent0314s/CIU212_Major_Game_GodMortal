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

    public ShieldManager shieldSetting;

    [Space]
    [Header("UI")]
    public Image UI_HPBar;
    public Image UI_StaminaBar;
    public Image UI_ShieldBar;
  

    public override void Initialzation()
    {
        base.Initialzation();
        shieldSetting.Initialization();
        currentStamina = maxStamina;
        UpdatePlayerHpBar();
        UpdatePlayerShieldBar();
    }

    private void Update()
    {
        shieldSetting.RecoverShield(()=> UpdatePlayerShieldBar());
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

    //public void AddShield() {
    //    shieldSetting.RecoverShield();
    //    UpdatePlayerShieldBar();
    //}


    public override void GetHurt(float damage)
    {
        if (shieldSetting.HasShield)
        {
            shieldSetting.GetHurt(damage);
            anim.Play("Hurt",0,0);
        }
        else {
            base.GetHurt(damage);
        }
        UpdatePlayerHpBar();
        UpdatePlayerShieldBar();
    }

    public void UpdatePlayerHpBar()
    {
        if (UI_HPBar)
        {
            UI_HPBar.fillAmount = healthSetting.GetHealthPercentage();
        }
    }

    public void UpdatePlayerShieldBar() {
        if (UI_ShieldBar) {
            UI_ShieldBar.fillAmount = shieldSetting.GetShieldPercentage();
        }
    }

    public void UpdatePlayerStBar() {
        if (UI_StaminaBar)
        {
            UI_StaminaBar.fillAmount = GetStaminaPercentage();
        }
    }

    public override void Dead()
    {
        GameFlowManager.i.GameOver();
    }
    public void CancelGravity() {
        rb.useGravity = false;
    }
}
