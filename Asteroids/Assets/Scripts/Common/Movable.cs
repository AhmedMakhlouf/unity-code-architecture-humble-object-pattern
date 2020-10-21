using UnityEngine;

public class Movable : IMovable
{
    public Vector3 Position { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 LookDirection { get; set; }

    private Rect screenspace;
    private MovementSettings settings;

    public Movable(MovementSettings settings)
    {
        this.settings = settings;

        var bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(
            Camera.main.pixelWidth, Camera.main.pixelHeight));

        screenspace = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
    }

    public void Move(float deltaTime)
    {
        Vector3 newPos = Position + Velocity * deltaTime;

        if (newPos.x > screenspace.x + screenspace.width)
            newPos.x = screenspace.x;

        if (newPos.x < screenspace.x)
            newPos.x = screenspace.x + screenspace.width;

        if (newPos.y > screenspace.y + screenspace.height)
            newPos.y = screenspace.y;

        if (newPos.y < screenspace.y)
            newPos.y = screenspace.y + screenspace.height;

        Position = newPos;

        if(settings.drag)
            Velocity -= Velocity * deltaTime;
    }
}