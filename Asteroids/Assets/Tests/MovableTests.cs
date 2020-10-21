using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MovableTests
    {
        [Test]
        public void Move_WithVelocity_ChangePosition()
        {
            var moveSettings = Resources.Load<MovementSettings>("Settings/PlayerShip");
            IMovable movable = new Movable(moveSettings);
            movable.Position = Vector3.zero;
            movable.Velocity = new Vector3(1.0f, 0.0f, 0.0f);

            movable.Move(1.0f);
            var actual = movable.Position;

            Assert.AreEqual(new Vector3(1.0f, 0.0f, 0.0f), actual);
        }
    }
}
