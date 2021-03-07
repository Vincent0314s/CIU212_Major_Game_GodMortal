﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void SetVelocity(Vector3 velocityVector);

    void SetJumpVelocity(Vector3 jumpVector);
}
