﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrajectoryFactory
{

    public static Vector3 HitTargetAtTime(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase, float timeToTarget)
    {
        Vector3 AtoB = targetPosition - startPosition;
        Vector3 horizontal = GetHorizontalVector(AtoB, gravityBase);
        float horizontalDistance = horizontal.magnitude;
        Vector3 vertical = GetVerticalVector(AtoB, gravityBase);
        float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));

        timeToTarget = Mathf.Min(AtoB.magnitude * timeToTarget, timeToTarget);

        float horizontalSpeed = horizontalDistance / timeToTarget;
        float verticalSpeed = (verticalDistance + ((0.5f * gravityBase.magnitude) * (timeToTarget * timeToTarget))) / timeToTarget;

        Vector3 launch = (horizontal.normalized * horizontalSpeed) - (gravityBase.normalized * verticalSpeed);
        return launch;
    }

    private static Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase)
    {
        Vector3 output;
        Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular);
        output = Vector3.Project(AtoB, perpendicular);
        return output;
    }

    private static Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase)
    {
        Vector3 output;
        output = Vector3.Project(AtoB, gravityBase);
        return output;
    }
}
