using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour
{
    private static ItemManager _i;
    public static ItemManager i
    {
        get {
            if (_i == null) {
                _i = GameObject.FindObjectOfType<ItemManager>();
            }
            return _i;
        }
    }
    [Space]
    [Header("UI")]
    public Text UI_Item1text;

    public float healthPotionHealAmount = 15f;
    public float staminaPotionHealAmount = 5f;

    private int healthPotionNumber;
    private int staminaPotionNumber;
    private int letterNumber;

    private void Start()
    {
        UpdateItemSlotNumber();
    }

    public void AddItems(Items _type) {
        switch (_type) {
            case Items.HealthPotion:
                healthPotionNumber += 1;
                break;
            case Items.StaminaPotion:
                staminaPotionNumber += 1;
                break;
            case Items.Letter:
                letterNumber += 1;
                break;
        }
        UpdateItemSlotNumber();
    }

    public void RemoveItems(Items _type)
    {
        switch (_type)
        {
            case Items.HealthPotion:
                if (healthPotionNumber>0) { 
                    healthPotionNumber -= 1;
                }
                break;
            case Items.StaminaPotion:
                if (staminaPotionNumber > 0) { 
                    staminaPotionNumber -= 1;
                }
                break;
            case Items.Letter:
                letterNumber -= 1;
                break;
        }
        UpdateItemSlotNumber();
    }

    public bool CanUsingItems(Items _type)
    {
        switch (_type)
        {
            case Items.HealthPotion:
                if (healthPotionNumber > 0)
                {
                    return true;
                }
                break;
            case Items.StaminaPotion:
                if (staminaPotionNumber > 0)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public void UpdateItemSlotNumber() {
        UI_Item1text.text = healthPotionNumber.ToString();
    }
}
