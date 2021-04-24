using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Levels {
    WarArea_01,
    WarArea_02,
    WarArea_03,
    MainHub_01,
    MainHub_02,
    MainHub_03,
    LifeArea_01,
    LifeArea_02,
    LifeArea_03,
    LifeArea_04,
    DeathArea_01,
    DeathArea_02,
    DeathArea_03,
    DeathArea_04,
}


public class LevelAreaInformation : MonoBehaviour
{
    public Levels levelArea;
    public Color areaColor = new Color(0.5f,0.5f,0.5f,0.3f);
    public Color respawnColor = new Color(1,0,1,0.3f);
    public float respawnPointSize = 0.25f;
    public Transform respawnPoint;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = areaColor;
        Gizmos.DrawCube(transform.position, transform.localScale);

        Gizmos.color = respawnColor;
        Gizmos.DrawSphere(respawnPoint.position, respawnPointSize);
    }
}
