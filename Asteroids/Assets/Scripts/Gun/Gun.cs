using UnityEngine;

public class Gun : IGun
{
    public float Power { get; set; }
    private float cooldownTime;
    private Vector3 position;
    private Vector3 direction;
    private ProjectileType bulletType;

    public Gun(ProjectileType bulletType)
    {
        this.bulletType = bulletType;
    }

    public void CoolDown(float timePassed)
    {
        cooldownTime -= timePassed;
    }

    public void Shoot()
    {
        if (cooldownTime > 0)
            return;

        // Spawn Bullet
        var bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        var bulletInstance = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
        var bulletView = bulletInstance.GetComponent<View>();
        var movementSettings = Resources.Load<MovementSettings>("Settings/Bullet");
        var projectile = new Projectile(bulletView, movementSettings);
        projectile.Movement.Position = position;
        projectile.Movement.Velocity = direction * 10;
        projectile.Type = bulletType;
        var bullet = new Bullet(bulletView, projectile);
        

        //TOOD: Get cooldown value from gun settings (scriptable object)
        cooldownTime = 0.1f;
    }

    public void Aim(Vector3 position, Vector3 direction)
    {
        this.position = position;
        this.direction = direction;
    }

    public bool CanShoot()
    {
        return cooldownTime <= 0;
    }
}