using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public EnemyType type;
    GameObject enemyPrefab;

    public bool turnOnVisualizer = false;
    public Color visualColor = new Color(1,0,0,0.35f);
    public float visualRadiusSize = 1.5f;


    public void SpawnEnemy() {
        if (gameObject.activeInHierarchy && transform.childCount < 1) {
            switch (type)
            {
                case EnemyType.Melee:
                    enemyPrefab = Resources.Load<GameObject>("Enemy/Enemy_Melee");
                    GameObject m = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    m.transform.parent = transform;
                    break;
                case EnemyType.Ranged:
                    enemyPrefab = Resources.Load<GameObject>("Enemy/Enemy_Ranged");
                    GameObject r = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    r.transform.parent = transform;
                    break;
                case EnemyType.Flying:

                    break;
            }
        }
       
    }

    private void OnDrawGizmos()
    {
        if (turnOnVisualizer) {
            Gizmos.color = visualColor;
            Gizmos.DrawSphere(transform.position, visualRadiusSize);
        }
    }
}

