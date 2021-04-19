using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelCheckPoint : MonoBehaviour
{
    public enum CheckPointType { 
        Horizontal,
        Vertical
    }
    public LevelCheckState levelState;
    public CheckPointType type;

    public bool canActivatePositiveIndex = true;
    public int[] levelToShowIndex_positive;
    public bool canActivateNegativeIndex = false;
    public int[] levelToShowIndex_negative;


    public Color startPointColor = Color.cyan;
    public Color checkPointColor = Color.green;
    public Color endPointColor = Color.red;


    public CheckPointShape shape;
    public Vector3 size = new Vector3(1.5f, 0.5f, 5f);
    public float radius = 1.5f;

    public Action activateLevelEvent;
    private Transform player;

    private void Start()
    {
        player = GameAssetManager.i.player;
        
    }

    private void OnDrawGizmos()
    {
        switch (shape)
        {
            case CheckPointShape.Box:
                switch (levelState)
                {
                    case LevelCheckState.StartPoint:
                        Gizmos.color = startPointColor;
                        break;
                    case LevelCheckState.CheckPoint:
                        Gizmos.color = checkPointColor;
                        break;
                    case LevelCheckState.EndPoint:
                        Gizmos.color = endPointColor;
                        break;
                }
                Gizmos.DrawCube(transform.position, size);
                break;
            case CheckPointShape.Sphere:
                switch (levelState)
                {
                    case LevelCheckState.StartPoint:
                        Gizmos.color = startPointColor;
                        break;
                    case LevelCheckState.CheckPoint:
                        Gizmos.color = checkPointColor;
                        break;
                    case LevelCheckState.EndPoint:
                        Gizmos.color = endPointColor;
                        break;
                }
                Gizmos.DrawSphere(transform.position, radius);
                break;
        }
    }

    public void ActivatePositiveLevels() {
        if (canActivatePositiveIndex)
        {
            for (int i = 0; i < levelToShowIndex_positive.Length; i++)
            {
                LevelManager.i.SetLevelActive(levelToShowIndex_positive[i], true);
            }
        }
        if (canActivateNegativeIndex)
        {
            for (int i = 0; i < levelToShowIndex_negative.Length; i++)
            {
                LevelManager.i.SetLevelActive(levelToShowIndex_negative[i], false);
            }
        }
    }

    public void ActivateNegativeLevels() {
        if (canActivatePositiveIndex)
        {
            for (int i = 0; i < levelToShowIndex_positive.Length; i++)
            {
                LevelManager.i.SetLevelActive(levelToShowIndex_positive[i], false);
            }
        }
        if (canActivateNegativeIndex)
        {
            for (int i = 0; i < levelToShowIndex_negative.Length; i++)
            {
                LevelManager.i.SetLevelActive(levelToShowIndex_negative[i], true);
            }

        }
    }

    private void Update()
    {
        if (levelState == LevelCheckState.CheckPoint)
        {
            if (type == CheckPointType.Horizontal)
            {
                if (player.position.x > transform.position.x && activateLevelEvent == null)
                {
                    activateLevelEvent = ActivatePositiveLevels;
                    activateLevelEvent?.Invoke();
                }
                else if (player.position.x < transform.position.x && activateLevelEvent != null)
                {
                    activateLevelEvent = ActivateNegativeLevels;
                    activateLevelEvent?.Invoke();
                    activateLevelEvent = null;
                }
            }
            else {
                if (player.position.y < transform.position.y && activateLevelEvent == null)
                {
                    activateLevelEvent = ActivatePositiveLevels;
                    activateLevelEvent?.Invoke();
                }
                else if (player.position.y > transform.position.y && activateLevelEvent != null)
                {
                    activateLevelEvent = ActivateNegativeLevels;
                    activateLevelEvent?.Invoke();
                    activateLevelEvent = null;
                }
            }
           
        }
    }
}
