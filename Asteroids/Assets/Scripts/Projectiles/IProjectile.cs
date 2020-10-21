using System;

public enum ProjectileType { PlayerShip, EnemyShip, Astroid, PlayerBullet, EnemyBullet }

public interface IProjectile : IDisposable
{
    IMovable Movement { get; set; }
    ProjectileType Type { get; set; }
}