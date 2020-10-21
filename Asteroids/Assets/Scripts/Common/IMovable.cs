using UnityEngine;
using System;

public interface IMovable
{
    Vector3 Position { get; set; }
    Vector3 Velocity { get; set; }
    Vector3 LookDirection { get; set; }
    void Move(float deltaTime);
}
