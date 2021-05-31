using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Ranged : EnemyController
{


    public Material temp;


    public override void Start()
    {
        base.Start();
        for (int i = 0; i < GetComponentsInChildren<MeshRenderer>().Length; i++)
        {
            GetComponentsInChildren<MeshRenderer>()[i].material = temp;
        }
        
    }
    public override void Idle_Update()
    {
        if (player)
        {
            if (currentReactionTimer < reactionTimer)
            {
                currentReactionTimer += Time.deltaTime;
            }
            else
            {
                if (IsInAttackRange(player.position))
                {
                    Attack();
                }
                else {
                    TracingPlayer();
                }
            }
        }
        else
        {
            if (!IsCloseToTarget(originalPosition))
            {
                Move(originalPosition);
            }
        }
    }

    public override void Move_Upate()
    {
        if (player)
        {
            if (IsInAttackRange(player.position))
            {
                Attack();
            }
        }
        else
        {
            if (IsCloseToTarget(originalPosition))
            {
                cm.StopMoving();
            }
            else
            {
                Move(originalPosition);
            }
        }
    }
}
