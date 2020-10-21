using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFollower
{
    Vector3 GetDistination(Vector3 position);

    bool ReachedFinalDistination(Vector3 position);
}
