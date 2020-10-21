using System;
using UnityEngine;

public class Projectile : IProjectile
{
    public ProjectileType Type 
    {
        get { return type; }
        set
        {
            type = value;
            view.Type = type;
        }
    }
    private ProjectileType type;
    public IMovable Movement { get; set; }

    protected readonly IView view;

    public Projectile(IView view, MovementSettings moveSettings)
    {
        Movement = new Movable(moveSettings);

        this.view = view;
        this.view.OnUpdate += OnUpdate;
    }

    private void OnUpdate(object sender, UpdateEventArgs e)
    {
        Movement.Move(e.DeltaTime);

        view.Position = Movement.Position;
    }

    public void Dispose()
    {
        view.OnUpdate -= OnUpdate;
        view.Dispose();
    }
}
