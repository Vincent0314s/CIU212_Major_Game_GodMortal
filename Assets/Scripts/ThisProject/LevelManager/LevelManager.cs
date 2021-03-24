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

    public GameObject levelParentToLoad;
    public GameObject[] levels;

    public void UpdateList() {
        levels = new GameObject[levelParentToLoad.transform.childCount];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = levelParentToLoad.transform.GetChild(i).gameObject;
        }
    }

    public void SetLevelActive(int _index,bool _condition) {
        if (_index != -1) { 
            levels[_index].SetActive(_condition);
        }
    }
}
