using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour
{

    PlayerController pc;

    private void OnEnable()
    {
        pc = GameAssetManager.i.currentPlayer.GetComponent<PlayerController>();
        if (pc.whereisPlayer == Levels.LifeArea_03 || pc.whereisPlayer == Levels.DeathArea_03)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        else {
            GetComponent<UnityEngine.UI.Button>().interactable = true;
        }
    }
}
