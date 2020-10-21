using System;

public interface IEnemyShip : IDisposable
{
    event EventHandler<EnemyShipHitEventArgs> OnHit;
    IShip Ship { get; set; }
}