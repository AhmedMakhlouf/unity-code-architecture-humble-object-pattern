using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PathFollowerTests
    {
        [Test]
        public void ReachedFinalDistination_GivenFinalPosition_ReturnTrue()
        {
            var path = new Vector3[]
            {
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 1.0f, 1.0f)
            };

            var pathFollower = new PathFollower(path);
            bool reachedFinalDistination = pathFollower.ReachedFinalDistination(new Vector3(1.0f, 1.0f, 1.0f));

            Assert.IsTrue(reachedFinalDistination);
        }


        [Test]
        public void GetDistination_GivePosition_ReturnsNextDistination()
        {
            var path = new Vector3[]
            {
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(2.0f, 2.0f, 2.0f),
                new Vector3(3.0f, 3.0f, 3.0f)
            };

            var pathFollower = new PathFollower(path);

            var actual = pathFollower.GetDistination(new Vector3(0.0f, 0.0f, 0.0f));

            Assert.AreEqual(new Vector3(1.0f, 1.0f, 1.0f), actual);
        }

        [Test]
        public void GetDistination_GivenTwoPositionsAlongThePath_ReturnsThirdDistination()
        {
            var path = new Vector3[]
            {
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(2.0f, 2.0f, 2.0f),
                new Vector3(3.0f, 3.0f, 3.0f)
            };

            var pathFollower = new PathFollower(path);

            pathFollower.GetDistination(new Vector3(0.0f, 0.0f, 0.0f));
            var actual = pathFollower.GetDistination(new Vector3(1.0f, 1.0f, 1.0f));

            Assert.AreEqual(new Vector3(2.0f, 2.0f, 2.0f), actual);
        }
    }
}
