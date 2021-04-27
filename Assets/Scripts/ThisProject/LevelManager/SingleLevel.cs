using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleLevel : MonoBehaviour
{
    public Levels levelArea;
    public int[] levelsToOpen;
    public int[] levelsToClose;
    public Color areaBorderColor = new Color(1,0.9f,0.02f,0.35f);


    public void MatchGameobjectName() {
        if (gameObject.name != levelArea.ToString()) { 
            gameObject.name = levelArea.ToString();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = areaBorderColor;
        Gizmos.DrawCube(transform.GetChild(0).position, transform.localScale);
    }

    public void CallTrigger(Collider _c) {
        if (_c.transform.tag == "Player") {
            _c.GetComponent<PlayerController>().whereisPlayer = levelArea;
            for (int i = 0; i < levelsToOpen.Length; i++)
            {
                LevelManager.i.SetLevelActive(levelsToOpen[i], true);
            }

            for (int i = 0; i < levelsToClose.Length; i++)
            {
                LevelManager.i.SetLevelActive(levelsToClose[i], false);
            }
        }
    }
}
