using UnityEngine;
using System.Collections;
using System;

public class View : MonoBehaviour, IView
{
    [SerializeField] public ProjectileType Type { get; set; }
    public Vector3 Position { get { return transform.position; } set { transform.position = value; } }
    public Vector3 Scale { get { return transform.localScale; } set { transform.localScale = value; } }
    public Vector3 Direction { get { return transform.up; } set { transform.up = value; } }

    public bool Active { set { gameObject.SetActive(value); } }

    public event EventHandler<UpdateEventArgs> OnUpdate = (sender, e) => { };
    public event EventHandler<CollisionEventArgs> OnCollision = (sender, e) => { };
    public event EventHandler<ProjectileCollisionEventArgs> OnProjectileCollision = (sender, e) => { };

    //private new Transform transform;

    //private void Awake()
    //{
    //    transform = GetComponent<Transform>();
    //}

    private void Update()
    {
        OnUpdate(this, new UpdateEventArgs(Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision(this, new CollisionEventArgs(other, other.GetComponent<IView>().Type));
    }

    public void ProjectileCollision(IProjectile projectile)
    {
        OnProjectileCollision(this, new ProjectileCollisionEventArgs(projectile));
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}

