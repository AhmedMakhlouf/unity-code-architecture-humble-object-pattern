using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GunTests
    {
        [Test]
        public void CanShoot_ShootTwiceWithoutCoolingDown_ReturnFalse()
        {
            var gun = new Gun(ProjectileType.PlayerBullet);

            gun.Shoot();
            var canShoot = gun.CanShoot();

            Assert.IsFalse(canShoot);
        }

        [Test]
        public void CanShoot_ShootCoolDownThenShootAgain_ReturnTrue()
        {
            var gun = new Gun(ProjectileType.PlayerBullet);

            gun.Shoot();
            gun.CoolDown(0.1f);
            var canShoot = gun.CanShoot();

            Assert.IsTrue(canShoot);
        }

    }
}
