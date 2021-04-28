using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : GameBasicFlowManager
{
    private static GameFlowManager _i;
    public static GameFlowManager i
    {
        get {
            if (_i == null) {
                _i = FindObjectOfType<GameFlowManager>();
            }
            return _i;
        }
    }
    public GameObject UI_GameOver;
    public GameObject UI_PauseMenu;

 

    public void GameOver() {
        UI_GameOver.SetActive(true);
    }

    public void OpenPauseMenu() {
        if (!UI_PauseMenu.activeInHierarchy) {
            Time.timeScale = 0;
            UI_PauseMenu.SetActive(true);
        }
    }

    public void Resume() {
        if (UI_PauseMenu.activeInHierarchy)
        {
            Time.timeScale = 1;
            UI_PauseMenu.SetActive(false);
        }
    }

}
