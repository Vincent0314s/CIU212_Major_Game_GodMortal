using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_Ground : Character_GroundMovement
{
    private void Update()
    {
        Flip();
        //OnGround(true);
    }
}
