using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    PlayerController pc;

    void OnEnable()
    {
        pc = GameAssetManager.i.currentPlayer.GetComponent<PlayerController>();
        if (pc.whereisPlayer == Levels.LifeArea_03 || pc.whereisPlayer == Levels.DeathArea_03)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            if (!SaveLoadSystem.HasSaveData())
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }
    }
}
