using UnityEngine;
using System;

public class Ship : IShip
{
	public IProjectile Projectile { get; set; }
	public IGun Gun { get; set; }

	private IView view;

	public Ship(IView view, IGun gun, MovementSettings moveSettings)
	{
		Gun = gun;
		this.view = view;
		this.view.OnUpdate += OnUpdate;
		Projectile = new Projectile(view, moveSettings);
		Projectile.Type = ProjectileType.PlayerShip;
	}
	private void OnUpdate(object sender, UpdateEventArgs e)
	{
		Gun.CoolDown(e.DeltaTime);
		Gun.Aim(Projectile.Movement.Position, Projectile.Movement.LookDirection);
	}

	public void Dispose()
	{
		view.OnUpdate -= OnUpdate;
		Projectile.Dispose();
	}
}
