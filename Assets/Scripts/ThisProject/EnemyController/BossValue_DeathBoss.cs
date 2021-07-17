using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossValue_DeathBoss : CharacterBaseValue
{

    [Header("UI")]
    public Image UI_HPBar;

    float t;

    private void Update()
    {
        UpdateHealthBarLerp();
    }

    public override void GetHurt(float _damage, bool _canPlayAnimation)
    {
        base.GetHurt(_damage, _canPlayAnimation);
        UpdateHealthBar();
    }

    public void UpdateHealthBarLerp() {
        if (UI_HPBar && t < 1)
        {
            UI_HPBar.fillAmount = Mathf.Lerp(0,1,t);
            t += 0.5f * Time.deltaTime;
        }
    }

    public void UpdateHealthBar()
    {
        if (UI_HPBar)
        {
            UI_HPBar.fillAmount = healthSetting.GetHealthPercentage();
        }
    }
    public override void Dead()
    {
        GameFlowManager.i.isKillingDeathBoss = true;
        GetComponent<Character_FlyingMovement>().StopMoving();
        transform.parent = null;
        rb.useGravity = true;
        GetComponent<Collider>().enabled = true;
        GameFlowManager.i.AutoSaving();
        Destroy(this.gameObject, 5f);
    }
}
