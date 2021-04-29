using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class HealthManager
{
    public float maxHP;
    private float currentHP;

    private Action OnDeadEvent;

    public HealthManager(float _currentHP, float _maxHP) {
        currentHP = _currentHP;
        maxHP = _maxHP;
    }

    public float GetHealthPercentage() {
        return currentHP / maxHP;
    }

    public void AddHP(float _amount) {
        currentHP += _amount;
        if (currentHP >= maxHP) {
            currentHP = maxHP;
        } else if (currentHP <= 0) {
            currentHP = 0;
            OnDeadEvent?.Invoke();
        }
    }

    public void GetHurt(float _damage) {
        AddHP(-_damage);
    }
}
