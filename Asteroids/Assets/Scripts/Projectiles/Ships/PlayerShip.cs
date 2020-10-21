using System;
using UnityEngine;

public class PlayerShip : IPlayerShip
{
	public event EventHandler<PlayerShipHitEventArgs> OnHit;

	public IShip Ship { get; set; }
	public bool Shield {
		set
		{
			if (value != shield)
			{
				shield = value;
				shieldView.Shield = shield;

				if (shield)
					shieldTimer = 1.0f;
			}
		}
		get { return shield; }
	}
	private bool shield = false;
	private float shieldTimer = 0.0f;
	private IView view;
	private IShieldView shieldView;

	public PlayerShip(IView view, IShieldView shieldView, IInputReader inputReader)
	{
		var movementSettings = Resources.Load<MovementSettings>("Settings/PlayerShip");

		Ship = new Ship(view, new Gun(ProjectileType.PlayerBullet), movementSettings);
		Ship.Projectile.Type = ProjectileType.PlayerShip;

		this.shieldView = shieldView;
		this.view = view;
		inputReader.OnInput += OnInput;
		shieldView.OnCollision += OnCollision;
		this.view.OnUpdate += OnUpdate;
		this.view.Position = Ship.Projectile.Movement.Position;
		this.view.Active = true;
	}

	private void OnUpdate(object sender, UpdateEventArgs e)
	{
		view.Direction = Ship.Projectile.Movement.LookDirection;

		if(shieldTimer > 0)
			shieldTimer -= e.DeltaTime;
		
		Shield = shieldTimer > 0;
	}
	
	private void OnCollision(object sender, CollisionEventArgs e)
	{
		if (Shield)
			return;

		OnHit(this, new PlayerShipHitEventArgs(this, e.Projectile));
	}

	private void OnInput(object sender, InputEventArgs e)
	{
		e.Command.Excute(Ship.Projectile.Movement, Ship.Gun);
	}
}

