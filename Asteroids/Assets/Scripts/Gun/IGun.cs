using System;
using UnityEngine;

public interface IGun
{
    float Power { get; set; }

    void CoolDown(float timePassed);
    void Shoot();
    void Aim(Vector3 position, Vector3 direction);
    bool CanShoot();
}