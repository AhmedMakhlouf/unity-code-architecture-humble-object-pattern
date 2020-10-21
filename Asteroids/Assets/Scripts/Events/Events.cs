using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventArgs : EventArgs
{
    public Collider Collider { get; set; }
    public ProjectileType Projectile { get; set; }

    public CollisionEventArgs(Collider collider, ProjectileType otherProjectile)
    {
        Collider = collider;
        Projectile = otherProjectile;
    }
}

public class UpdateEventArgs : EventArgs
{
    public float DeltaTime { get; set; }

    public UpdateEventArgs(float deltaTime)
    {
        DeltaTime = deltaTime;
    }
}

//public class InputEventArgs<T> : EventArgs
//{
//    public ICommand<T> Command { get; set; }

//    public InputEventArgs(ICommand<T> command)
//    {
//        Command = command;
//    }
//}

public class InputEventArgs : EventArgs
{
    public ICommand Command { get; set; }

    public InputEventArgs(ICommand command)
    {
        Command = command;
    }
}

public class ProjectileCollisionEventArgs : EventArgs
{
    public IProjectile Projectile { get; set; }

    public ProjectileCollisionEventArgs(IProjectile projectile)
    {
        Projectile = projectile;
    }
}

public class AstroidHitEventArgs : EventArgs
{
    public IAstroid Astroid { get; set; }
    public ProjectileType OtherProjectile { get; set; }

    public AstroidHitEventArgs(IAstroid astroid, ProjectileType otherProjectile)
    {
        Astroid = astroid;
        OtherProjectile = otherProjectile;
    }
}

public class PlayerShipHitEventArgs : EventArgs
{
    public IPlayerShip PlayerShip { get; set; }
    public ProjectileType OtherProjectile { get; set; }

    public PlayerShipHitEventArgs(IPlayerShip playerShip, ProjectileType otherProjectile)
    {
        PlayerShip = playerShip;
        OtherProjectile = otherProjectile;
    }
}

public class EnemyShipHitEventArgs : EventArgs
{
    public IEnemyShip EnemyShip { get; set; }
    public ProjectileType OtherProjectile { get; set; }

    public EnemyShipHitEventArgs(IEnemyShip enemyShip, ProjectileType otherProjectile)
    {
        EnemyShip = enemyShip;
        OtherProjectile = otherProjectile;
    }
}


