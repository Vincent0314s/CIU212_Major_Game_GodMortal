using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class StaminaValue
{

    public PlayerActionType actionType;
    public float amount;

    public StaminaValue(PlayerActionType _type) {
        actionType = _type;
    }
}


[System.Serializable]
public class StaminaManager
{
    public float maxStamina = 100f;
    private float currentStamina;

    public float recoverSpeed;

    [ArrayElementTitle("actionType")]
    public List<StaminaValue> staminaConsumeList = new List<StaminaValue> {
            new StaminaValue(PlayerActionType.Jump),
            new StaminaValue(PlayerActionType.DoubleJump),
            new StaminaValue(PlayerActionType.Dash),
            new StaminaValue(PlayerActionType.LightAttack),
            new StaminaValue(PlayerActionType.HeavyAttack),
            new StaminaValue(PlayerActionType.AerialAttack),
            new StaminaValue(PlayerActionType.RangedPower),
    };

    private float exhaustedDelayToRecover = 2.5f;
    private float exhaustedCountTimer;
    private bool isOutofExhausted;

    public void Initialization()
    {
        currentStamina = maxStamina;
    }
    public float GetStaminaPercentage()
    {
        return currentStamina / maxStamina;
    }

    public void RecoverStamina(Action _OnUpdateValue)
    {
        if (!CanConsumeStamina())
        {
            isOutofExhausted = true;
        }
        if (isOutofExhausted)
        {
            if (exhaustedCountTimer <= exhaustedDelayToRecover)
            {
                exhaustedCountTimer += Time.deltaTime;
            }
            else
            {
                currentStamina = 1;
                exhaustedCountTimer = 0;
                isOutofExhausted = false;
            }
        }
        else
        {
            if (currentStamina < maxStamina)
            {
                StaminaConsume(-recoverSpeed, true);
            }
            else
            {
                currentStamina = maxStamina;
            }
        }
        _OnUpdateValue?.Invoke();
    }

    public void ConsumeStamina(PlayerActionType _type)
    {
        switch (_type)
        {
            case PlayerActionType.Jump:
                StaminaConsume(staminaConsumeList[0].amount, false);
                break;
            case PlayerActionType.DoubleJump:
                StaminaConsume(staminaConsumeList[1].amount, false);
                break;
            case PlayerActionType.Dash:
                StaminaConsume(staminaConsumeList[2].amount, false);
                break;
            case PlayerActionType.LightAttack:
                StaminaConsume(staminaConsumeList[3].amount, false);
                break;
            case PlayerActionType.HeavyAttack:
                StaminaConsume(staminaConsumeList[4].amount, false);
                break;
            case PlayerActionType.AerialAttack:
                StaminaConsume(staminaConsumeList[5].amount, false);
                break;
            case PlayerActionType.RangedPower:
                StaminaConsume(staminaConsumeList[6].amount, false);
                break;
        }
    }

    public bool CanConsumeStamina()
    {
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            return false;
        }
        return true;
    }

    private void StaminaConsume(float stamina, bool isConsumOverTime)
    {
        if (isConsumOverTime)
        {
            currentStamina -= stamina * Time.deltaTime;
        }
        else
        {
            currentStamina -= stamina;
        }
    }
}
