using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleLevel : MonoBehaviour
{
    public Levels levelArea;
    public int[] levelsToOpen;
    public int[] levelsToClose;
    public Color areaBorderColor = new Color(1,0.9f,0.02f,0.35f);
    public float stayAreaTime = 0;


    public void MatchGameobjectName() {
        if (gameObject.name != levelArea.ToString()) { 
            gameObject.name = levelArea.ToString();
        }
    }

    private void OnDrawGizmos()
    {
        if (GetComponentInParent<LevelAreaInformationParent>().turnOnVisualizer) {
            Gizmos.color = areaBorderColor;
            Gizmos.DrawCube(transform.GetChild(0).position, transform.localScale);
        }
       
    }

    public void CallTrigger(Collider _c) {
        if (_c.transform.tag == "Player") {
            _c.GetComponent<PlayerController>().whereisPlayer = levelArea;
            _c.GetComponent<PlayerController>().respawonPosition = transform.GetChild(1).position;
            GameFlowManager.i.DisplayArea(levelArea.ToString());
            if (levelArea != Levels.LifeArea_03 || levelArea != Levels.DeathArea_03)
            {
                _c.GetComponent<PlayerController>().SetPlayerInBossRoom(false);
                GameFlowManager.i.AutoSaving();
            }
            else {
                _c.GetComponent<PlayerController>().SetPlayerInBossRoom(true);
            }

            for (int i = 0; i < levelsToOpen.Length; i++)
            {
                if (!LevelManager.i.levels[levelsToOpen[i]].activeInHierarchy)
                {
                    LevelManager.i.SetLevelActive(levelsToOpen[i], true);
                }
            }

            for (int i = 0; i < levelsToClose.Length; i++)
            {
                if (LevelManager.i.levels[levelsToClose[i]].activeInHierarchy)
                {
                    LevelManager.i.SetLevelActive(levelsToClose[i], false);
                }
            }
        }
    }

    public void CallTriggerStay(Collider _c) {
        if (_c.transform.tag == "Player") {
            if (!_c.GetComponent<PlayerValue>().healthSetting.IsDead)
            {
                stayAreaTime += Time.unscaledDeltaTime;
            }
        }
    }
}
