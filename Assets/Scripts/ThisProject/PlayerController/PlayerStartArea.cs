using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartArea : MonoBehaviour
{

    public Levels whereisPlayer;

    void Start()
    {
        for (int i = 0; i < LevelManager.i.levelinfos.Length; i++)
        {
            if (whereisPlayer == LevelManager.i.levelinfos[i].GetComponent<SingleLevel>().levelArea) {
                transform.position = LevelManager.i.levelinfos[i].transform.GetChild(1).position;
            }
        }
    }

}
