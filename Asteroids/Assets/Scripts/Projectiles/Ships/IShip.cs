using System;

public interface IShip : IDisposable {
    IProjectile Projectile { get; set; }
    IGun Gun { get; set; }
}