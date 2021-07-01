using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Melee : EnemyController
{

    private float currentConsiderationTime;
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
            if (IsTargetOutOfRange(player.position))
            {
                StopTracinPlayer();
            }
            else {
                if (IsCloseToConsiderRange(player.position))
                {
                    if (currentConsiderationTime < 3.5f)
                    {
                        currentConsiderationTime += Time.deltaTime;
                    }
                    else {
                        currentConsiderationTime = 0;
                        TracingPlayer();
                    }
                }
                else {
                    TracingPlayer();
                }
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
