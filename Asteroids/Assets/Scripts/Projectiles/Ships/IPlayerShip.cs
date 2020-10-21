using System;

public interface IPlayerShip
{
    event EventHandler<PlayerShipHitEventArgs> OnHit;
    IShip Ship { get; set; }
    bool Shield { get; set; }
}