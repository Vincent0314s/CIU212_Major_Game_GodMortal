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
                //if (IsCloseToChangingCircle(player.position))
                //{
                //    Attack();
                //}
                //else
                //{
                //    TracingPlayer();
                //}
            }
        }
        else
        {
            //if (!IsCloseToChangingCircle(originalPosition))
            //{
            //    Move(originalPosition);
            //}
        }
    }

    public override void Move_Upate()
    {
        //if (player)
        //{
        //    if (IsCloseToChangingCircle(player.position))
        //    {
        //        Attack();
        //    }
        //}
        //else
        //{
        //    if (IsCloseToChangingCircle(originalPosition))
        //    {
        //        cm.StopMoving();
        //    }
        //    else
        //    {
        //        Move(originalPosition);
        //    }
        //}
    }
}
