using System;

public interface IBullet : IDisposable
{
    IProjectile Projectile { get; set; }
}