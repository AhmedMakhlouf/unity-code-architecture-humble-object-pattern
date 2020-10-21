using UnityEngine;
using System.Collections;

public class ShipInputCommand : ICommand
{
    public float Go { get; set; }
    public float Steering { get; set; }
    public float DeltaTime { get; set; }
    public bool Shoot { get; set; }

    public ShipInputCommand(float go, float steering, bool shoot, float deltaTime)
    {
        Go = go;
        Steering = steering;
        DeltaTime = deltaTime;
        Shoot = shoot;
    }

    public void Excute(IMovable movement, IGun gun)
    {
        ChangeVelocity(movement, Go, Steering, DeltaTime);

        if (Shoot)
            gun.Shoot();
    }

    private void ChangeVelocity(IMovable movement, float go, float steering, float deltaTime)
    {
        var angle = Mathf.Atan2(movement.LookDirection.x, movement.LookDirection.y) * (180 / Mathf.PI);

        if (steering != 0)
        {
            angle += steering * 300 * deltaTime % 360.0f;
            movement.LookDirection = new Vector3(Mathf.Sin(angle * (Mathf.PI / 180)), Mathf.Cos(angle * (Mathf.PI / 180)), 0.0f);
        }

        Vector3 additionalVelocity = Vector3.zero;

        if (go != 0)
        {
            additionalVelocity = new Vector3(75.0f * Mathf.Sin(angle * (Mathf.PI / 180)), 75.0f * Mathf.Cos(angle * (Mathf.PI / 180)), 0.0f) * deltaTime;
        }

        float speed = movement.Velocity.magnitude + additionalVelocity.magnitude;
        if (speed > 0.1f)
        {
            additionalVelocity *= 0.1f / speed;
        }

        movement.Velocity += additionalVelocity;
    }
}
