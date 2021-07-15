using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _i;
    public static LevelManager i {
        get {
            if (_i == null) {
                _i = GameObject.FindObjectOfType<LevelManager>();
            }
            return _i;
        }
    }

    public GameObject[] levelsToStart;

    public GameObject levelLayoutParentToLoad;
    public GameObject[] levels;

    public GameObject levelInformationParentToLoad;
    public GameObject[] levelinfos;

    private List<EnemySpawnPoint> enemyPoints = new List<EnemySpawnPoint>();
    private void Awake()
    {
        for (int i = 0; i < GameObject.FindObjectsOfType<EnemySpawnPoint>().Length; i++)
        {
            enemyPoints.Add(GameObject.FindObjectsOfType<EnemySpawnPoint>()[i]);
        }
    }

    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        //for (int i = 0; i < levelsToStart.Length; i++)
        //{
        //    levelsToStart[i].SetActive(true);
        //}
    }


    public void UpdateLayoutList() {
        levels = new GameObject[levelLayoutParentToLoad.transform.childCount];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = levelLayoutParentToLoad.transform.GetChild(i).gameObject;
        }
     
    }

    public void UpdateLevelInfoList() { 
        levelinfos = new GameObject[levelInformationParentToLoad.transform.childCount];
        for (int i = 0; i < levelinfos.Length; i++)
        {
            levelinfos[i] = levelInformationParentToLoad.transform.GetChild(i).gameObject;
        }
    }

    public void SetLevelActive(int _index, bool _condition) {
        if (_index != -1 && _index < levels.Length)
        {
            levels[_index].SetActive(_condition);
            for (int i = 0; i < enemyPoints.Count; i++)
            {
                enemyPoints[i].SpawnEnemy();
            }
        }
    }

    public void RespawnPlayer() {
        for (int i = 0; i < enemyPoints.Count; i++)
        {
            enemyPoints[i].SpawnEnemy();
        }
        //GameAssetManager.i.currentPlayer.GetComponent<CharacterBaseValue>().Initialzation();
        GameAssetManager.i.currentPlayer.GetComponent<PlayerValue>().ResetPlayerValue();
        GameAssetManager.i.currentPlayer.GetComponent<PlayerController>().InitPlayerController();
    }
}
