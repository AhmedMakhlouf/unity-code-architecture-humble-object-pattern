using System;
public enum AstroidSize { Small, Medium, Large }

public interface IAstroid : IDisposable
{
    event EventHandler<AstroidHitEventArgs> OnHit;
    IProjectile Projectile { get; set; }
    AstroidSize Size { get; set; }

}