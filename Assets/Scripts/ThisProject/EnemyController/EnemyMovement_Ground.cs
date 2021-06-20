using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_Ground : CharacterMovement
{
    private void Update()
    {
        Flip();
        OnGround(true);
    }
}
