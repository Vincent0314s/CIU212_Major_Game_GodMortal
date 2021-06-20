using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerValue : CharacterBaseValue
{

    [Header("Stamina")]
    public StaminaManager staminaSetting = new StaminaManager();

    [Space]
    [Header("Shield")]
    public ShieldManager shieldSetting = new ShieldManager();

    [Space]
    [Header("UI")]
    public Image UI_HPBar;
    public Image UI_StaminaBar;
    public Image UI_ShieldBar;

    public CapsuleCollider selfCollider;

    PlayerMovement pm;

    public override void Initialzation()
    {
        base.Initialzation();
        selfCollider = GetComponent<CapsuleCollider>();
        pm = GetComponent<PlayerMovement>();
        shieldSetting.Initialization();
        staminaSetting.Initialization();
        UpdatePlayerHpBar();
        UpdatePlayerShieldBar();
    }

    private void Update()
    {
        shieldSetting.RecoverShield(()=> UpdatePlayerShieldBar());
        staminaSetting.RecoverStamina(()=> UpdatePlayerStaminaBar());
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


    public override void GetHurt(float damage, bool _canPlayAnimation)
    {
        if (shieldSetting.HasShield)
        {
            shieldSetting.GetHurt(damage);
            if (_canPlayAnimation) { 
               anim.Play("Hurt", 0, 0);
            }
        }
        else
        {
            base.GetHurt(damage, _canPlayAnimation);
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

    public void UpdatePlayerStaminaBar() {
        if (UI_StaminaBar)
        {
            UI_StaminaBar.fillAmount = staminaSetting.GetStaminaPercentage();
        }
    }

    public override void Dead()
    {
        GameFlowManager.i.GameOver();
    }
 
}
