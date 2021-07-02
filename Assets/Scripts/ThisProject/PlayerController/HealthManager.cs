using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class HealthManager
{
    public float maxHP = 100f;
    public float currentHP { get; private set; }
    private bool isDead;
    public bool IsDead
    {
        get { return currentHP <= 0; }
        private set {
            isDead = value;
        }
    }

    public Action OnDeadEvent;

    public void SetCurrentHP(float _newHP) {
        currentHP = _newHP;
    }

    public void Initialization() {
        currentHP = maxHP;
        isDead = false;
    }

    public float GetHealthPercentage() {
        return currentHP / maxHP;
    }

    public void GetHeal(float _amount) {
        currentHP += _amount;
        if (currentHP >= maxHP) {
            currentHP = maxHP;
        } else if (currentHP <= 0) {
            currentHP = 0;
            OnDeadEvent?.Invoke();
        }
    }

    public void GetHeal(float _amount, Action _OnHurtEvent, Action _OnDeadEvent)
    {
        currentHP += _amount;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;
        }
        else if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeadEvent?.Invoke();
        }
    }

    public void GetHurt(float _amount)
    {
        currentHP -= _amount;
        if (currentHP <= 0)
        {
            OnDeadEvent?.Invoke();
        }
    }

    public void GetHurt(float _amount, Action _OnHurtEvent, Action _OnDeadEvent)
    {
        currentHP -= _amount;
        if (currentHP > 0)
        {
            _OnHurtEvent?.Invoke();
        }
        else if (currentHP <= 0)
        {
            _OnDeadEvent?.Invoke();
            OnDeadEvent?.Invoke();
            currentHP = 0;
        }
    }
}
