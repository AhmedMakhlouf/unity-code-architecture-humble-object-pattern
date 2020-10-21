using System;

public interface ICollidable
{
    event EventHandler<CollisionEventArgs> OnCollision;
}