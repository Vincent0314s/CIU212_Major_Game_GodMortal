using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    void OnEnable()
    {
        if (!SaveLoadSystem.HasSaveData())
        {
            GetComponent<Button>().interactable = false;
        }
        else {
            GetComponent<Button>().interactable = true;
        }
       
    }
}
