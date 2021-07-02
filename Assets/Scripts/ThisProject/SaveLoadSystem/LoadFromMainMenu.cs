using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFromMainMenu : MonoBehaviour
{

    public static bool isLoadFromMainMenu;

    public void LoadData() {
        isLoadFromMainMenu = true;
        SceneController.NextScene();
    }
}
