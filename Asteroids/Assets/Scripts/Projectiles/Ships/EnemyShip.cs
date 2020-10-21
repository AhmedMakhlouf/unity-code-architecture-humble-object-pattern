using UnityEngine;
using System;

public class EnemyShip : IEnemyShip
{
	public event EventHandler<EnemyShipHitEventArgs> OnHit;
	public IShip Ship { get; set; }

	private IPathFollower pathData;
	private IView view;
	private IView target;

	private float shootTimer = 0.0f;

	public EnemyShip(IView view, IView target, Vector3[] path)
	{
		this.target = target;
		var movementSettings = Resources.Load<MovementSettings>("Settings/EnemyShip");

		Ship = new Ship(view, new Gun(ProjectileType.EnemyBullet), movementSettings);
		Ship.Projectile.Type = ProjectileType.EnemyShip;
		Ship.Projectile.Movement.Position = path[0];

		pathData = new PathFollower(path);

		this.view = view;
		this.view.OnUpdate += OnUpdate;
		this.view.OnCollision += OnCollision;
		this.view.Position = Ship.Projectile.Movement.Position;
		this.view.Active = true;
	}

	private void OnCollision(object sender, CollisionEventArgs e)
	{
		OnHit(this, new EnemyShipHitEventArgs(this, e.Projectile));
	}

	public void Dispose()
	{
		Ship.Dispose();

		view.OnUpdate -= OnUpdate;
		view.OnCollision -= OnCollision;
		view.Dispose();
	}

	private void OnUpdate(object sender, UpdateEventArgs e)
	{
		shootTimer += e.DeltaTime;

		//TODO: get value from enemy settings (scriptable object)
		//TODO: Shoot at random direction when can't find target
		if(shootTimer >= 3.0f && target != null)
		{
			shootTimer = 0;
			Vector3 direction = (target.Position - Ship.Projectile.Movement.Position).normalized;
			Ship.Gun.Aim(Ship.Projectile.Movement.Position, direction);
			Ship.Gun.Shoot();
		}

		Vector3 distination = pathData.GetDistination(Ship.Projectile.Movement.Position);
		Ship.Projectile.Movement.Velocity = (distination - Ship.Projectile.Movement.Position).normalized;

		if (pathData.ReachedFinalDistination(Ship.Projectile.Movement.Position))
			Dispose();
	}
}
