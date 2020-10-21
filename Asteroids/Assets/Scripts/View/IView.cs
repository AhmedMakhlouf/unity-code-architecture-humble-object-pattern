using System;
using UnityEngine;

public interface IView : IUpdatable, ICollidable, IDisposable
{
    Vector3 Position { get; set; }
    Vector3 Scale { get; set; }
    Vector3 Direction { get; set; }
    ProjectileType Type { get; set; }
    bool Active { set; }
}