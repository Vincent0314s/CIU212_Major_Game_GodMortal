using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaminaController 
{
    private static float exhaustedDelayToRecover = 2.5f;
    private static float exhaustedCountTimer;
    private static bool isOutofExhausted;

    //Life amount impact on stamina
    private static float lifeImpactOnStamina;

    //Amount Impact on Chi
    private static float amount;

    public static void StaminaInitialSetting()
    {
        //PlayerValueControl.i.sv.staminaCurrentValue = PlayerValueControl.i.sv.staminaMaxValue;
    }

    //public static float StaminaBarCalculation()
    //{
    //    return PlayerValueControl.i.sv.staminaCurrentValue / PlayerValueControl.i.sv.staminaMaxValue;
    //}

    //public static string StaminaNumber()
    //{
    //    return PlayerValueControl.i.sv.staminaCurrentValue.ToString("0") + "/" + PlayerValueControl.i.sv.staminaMaxValue.ToString("0");
    //}

    //Return to animation_player
    public static void RecoverStamina()
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
                PlayerValue.i.currentStamina = 1;
                exhaustedCountTimer = 0;
                isOutofExhausted = false;
            }
        }
        else
        {
            if (PlayerValue.i.currentStamina < PlayerValue.i.maxStamina)
            {
                StaminaConsume(-PlayerValue.i.recoverSpeed, true);
            }
            else
            {
                PlayerValue.i.currentStamina = PlayerValue.i.maxStamina;
            }
        }
        PlayerValue.i.UpdatePlayerStBar();
    }

    public static void ConsumeStamina(PlayerActionType _type)
    {
        switch (_type)
        {
            case PlayerActionType.Jump:
                StaminaConsume(PlayerValue.i.staminaList[0].amount, false);
                break;
            case PlayerActionType.DoubleJump:
                StaminaConsume(PlayerValue.i.staminaList[1].amount, false);
                break;
            case PlayerActionType.Dash:
                StaminaConsume(PlayerValue.i.staminaList[2].amount, false);
                break;
            case PlayerActionType.LightAttack:
                StaminaConsume(PlayerValue.i.staminaList[3].amount, false);
                break;
            case PlayerActionType.HeavyAttack:
                StaminaConsume(PlayerValue.i.staminaList[4].amount, false);
                break;
            case PlayerActionType.AerialAttack:
                StaminaConsume(PlayerValue.i.staminaList[5].amount, false);
                break;
            case PlayerActionType.RangedPower:
                StaminaConsume(PlayerValue.i.staminaList[6].amount, false);
                break;
        }
        PlayerValue.i.UpdatePlayerStBar();
    }

    public static bool CanConsumeStamina()
    {
        if (PlayerValue.i.currentStamina <= 0)
        {
            PlayerValue.i.currentStamina = 0;
            return false;
        }
        return true;
    }

    private static void StaminaConsume(float stamina, bool isConsumOverTime)
    {
        if (isConsumOverTime)
        {
            PlayerValue.i.currentStamina -= stamina * Time.deltaTime;
        }
        else
        {
            PlayerValue.i.currentStamina -= stamina;
        }
    }
 
}
