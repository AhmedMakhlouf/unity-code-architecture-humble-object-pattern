using System;
using UnityEngine;

public class Astroid : IAstroid
{
    public event EventHandler<AstroidHitEventArgs> OnHit;
    public AstroidSize Size 
	{
		get { return size; }
		set
		{
			size = value;

			switch(size)
			{
				case AstroidSize.Small:
					view.Scale = Vector3.one;
					break;
				case AstroidSize.Medium:
					view.Scale = new Vector3(2, 2, 2);
					break;
				case AstroidSize.Large:
					view.Scale = new Vector3(3, 3, 3);
					break;
				default:
					break;
			}
		}
	}
    public IProjectile Projectile { get; set; }

	private AstroidSize size;
	private IView view;

    public Astroid(IView view, IProjectile projectile)
    {
		Projectile = projectile;
        
        view.OnCollision += OnCollision;
		this.view = view;

		view.Position = projectile.Movement.Position;
		view.Active = true;
    }

	private void OnCollision(object sender, CollisionEventArgs e)
	{
		OnHit(this, new AstroidHitEventArgs(this, e.Projectile));
	}

	public void Dispose()
	{
		view.OnCollision -= OnCollision;

		Projectile.Dispose();

		view.Dispose();
	}
}
