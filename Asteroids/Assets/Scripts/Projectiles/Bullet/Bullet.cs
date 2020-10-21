using System;
using UnityEngine;

public class Bullet : IBullet
{
    public IProjectile Projectile { get; set; }
    private float lifeTime = 0;
    private IView view;

    public Bullet(IView view, Projectile projectile)
    {
        this.view = view;
        Projectile = projectile;

        this.view.OnUpdate += OnUpdate;
        this.view.OnCollision += OnCollision;

        lifeTime = 0.5f;
        this.view.Position = projectile.Movement.Position;
        this.view.Active = true;
    }

    private void OnCollision(object sender, CollisionEventArgs e)
    {
        if (Projectile.Type == ProjectileType.PlayerBullet &&
            e.Projectile == ProjectileType.PlayerShip)
            return;

        if (Projectile.Type == ProjectileType.EnemyBullet &&
            e.Projectile == ProjectileType.EnemyShip)
            return;

        Dispose();
    }

    private void OnUpdate(object sender, UpdateEventArgs e)
    {
        lifeTime -= e.DeltaTime;

        if (lifeTime <= 0)
            Dispose();
    }

    public void Dispose()
    {
        view.OnUpdate -= OnUpdate;
        Projectile.Dispose();
        view.Dispose();
    }

}

