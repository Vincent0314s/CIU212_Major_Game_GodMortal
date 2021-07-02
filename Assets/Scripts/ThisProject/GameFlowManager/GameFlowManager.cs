using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlowManager : GameBasicFlowManager
{
    private class SaveObject
    {
        public Vector3 playerPosition;
        public float currentHP;
        public float currentShield;
        public int hpPotion;
    }


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

    private void Awake()
    {
        //Create Save Folder
        SaveLoadSystem.Init();
        if (LoadFromMainMenu.isLoadFromMainMenu) {
            LoadData();
        }

    }
    public void GameOver() {
        Time.timeScale = 0;
        UI_GameOver.SetActive(true);
    }

    public void OpenPauseMenu() {
        if (!UI_PauseMenu.activeInHierarchy) {
            UI_PauseMenu.SetActive(true);
            StartCoroutine(DelayToPauseTime());
        }
    }

    IEnumerator DelayToPauseTime() {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
    }

    public void Resume() {
        if (UI_PauseMenu.activeInHierarchy)
        {
            ResetTimeScale();
            UI_PauseMenu.SetActive(false);
        }
    }

    public void ResetTimeScale() {
        Time.timeScale = 1;
    }


    public void SaveData() {
        SaveObject saveObject = new SaveObject {
            playerPosition = GameAssetManager.i.currentPlayer.transform.position,
            currentHP = GameAssetManager.i.currentPlayer.GetComponent<PlayerValue>().healthSetting.currentHP,
            currentShield = GameAssetManager.i.currentPlayer.GetComponent<PlayerValue>().shieldSetting.currentShield,
            hpPotion = ItemManager.i.healthPotionNumber,
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveLoadSystem.Save(json);
    }

    public void LoadData(){
        string saveString = SaveLoadSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            GameAssetManager.i.currentPlayer.transform.position = saveObject.playerPosition;
            GameAssetManager.i.currentPlayer.GetComponent<PlayerValue>().healthSetting.SetCurrentHP(saveObject.currentHP);
            GameAssetManager.i.currentPlayer.GetComponent<PlayerValue>().shieldSetting.SetCurrentShield(saveObject.currentShield);
            GameAssetManager.i.currentPlayer.GetComponent<PlayerValue>().UpdateFromLoad();

            ItemManager.i.AddItems(Items.HealthPotion,saveObject.hpPotion,true);


            Resume();
        }
        else {
            Debug.LogError("NoSave");
        }
    }
}



