using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValue : CharacterBaseValue
{
    public override void Dead()
    {
        AnalyticsManager.AddEnemyDeathTimes();
        transform.parent = null;
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject, 5f);
    }
}
