﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetManager : MonoBehaviour
{
    private static GameAssetManager _i;
    public static GameAssetManager i {
        get {
            if (_i == null) {
                _i = GameObject.FindObjectOfType<GameAssetManager>();
            }
            return _i;
        }
    }

    public GameObject currentPlayer;
    public GameObject HP_Potion;

    private void Awake()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
    }

}
