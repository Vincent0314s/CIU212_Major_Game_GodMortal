using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
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

    public void GameOver() {
        UI_GameOver.SetActive(true);
    }


}
