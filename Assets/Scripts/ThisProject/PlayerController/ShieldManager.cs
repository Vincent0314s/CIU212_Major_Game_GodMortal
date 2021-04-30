using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ShieldManager
{
    public float maxShield = 50;
    private float currentShield;
    public float recoverAmount = 10;
    public float recoverBySec = 5f;
    private float currentRecoverSec;

    private bool hasShield;
    public bool HasShield
    {
        get { if (currentShield <= 0)
            {
                hasShield = false;
            }
            else {
                hasShield = true;
            }
            return hasShield; }
        private set {
            hasShield = value;
        }
    }

    public void Initialization() {
        currentShield = maxShield;
        currentRecoverSec = 0;
    }

    public float GetShieldPercentage() {
        return currentShield / maxShield;
    }
    public void GetHurt(float _damage) {
        currentShield -= _damage;
    }

    public void RecoverShield(Action _OnUpdateValue) {
        if (!HasMaxShield())
        {
            if (currentRecoverSec < recoverBySec)
            {
                currentRecoverSec += Time.deltaTime;
            }
            else
            {
                currentShield += recoverAmount;
                _OnUpdateValue?.Invoke();
                currentRecoverSec = 0;
            }
        }
        else {
            currentShield = maxShield;
        }
    }

    private bool HasMaxShield() {
        if (currentShield < maxShield)
        {
            return false;
        }
        return true;
    }
}
