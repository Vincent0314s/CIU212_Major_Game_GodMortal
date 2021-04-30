using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Flying : EnemyController
{
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
                if (IsCloseToTarget(player.position))
                {
                    Attack();
                }
                else
                {
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
            if (IsCloseToTarget(player.position))
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
