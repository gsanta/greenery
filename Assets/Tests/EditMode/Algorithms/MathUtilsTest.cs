using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Utils.math;

namespace Tests.EditMode.Algorithms
{
    public class MathUtilsTest
    {
        float TOLERANCE = 0.005f;

        [Test]
        public void GetRotationPI()
        {
            var rotation = MathUtils.GetAngle(new Vector2(0f, 0f), new Vector2(-1f, 0));
            Assert.AreEqual(Mathf.PI, rotation);
        }
        
        [Test]
        public void GetRotationPIOver2()
        {
            var rotation = MathUtils.GetAngle(new Vector2(0f, 0f), new Vector2(0f, 1f));
            Assert.AreEqual(Mathf.PI / 2, rotation);
        }
        
        [Test]
        public void GetRotation3PIOver2()
        {
            var rotation = MathUtils.GetAngle(new Vector2(0f, 0f), new Vector2(0f, -1f));
            Assert.AreEqual(3 * Mathf.PI / 2, rotation);
        }

        [Test]
        public void GetAngleDistanceAngle1SmallerThenAngle2()
        {
            var angle1 = Mathf.PI;
            var angle2 = 3f * Mathf.PI / 2f;
            var distance = MathUtils.GetAngleDistance(angle1, angle2);
            Assert.AreEqual(distance, 3f * Mathf.PI / 2f);
        }
        
        [Test]
        public void GetAngleDistanceAngle2SmallerThenAngle1()
        {
            var angle1 = 3f * Mathf.PI / 2f;
            var angle2 = Mathf.PI;
            var distance = MathUtils.GetAngleDistance(angle1, angle2);
            Assert.AreEqual(distance, Mathf.PI / 2f, TOLERANCE);
        }

        [Test]
        public void GetClosestLeftAngleWithManyAngles()
        {
            var referenceAngle = Mathf.PI;
            List<float> angles = new List<float>()
            {
                0f,
                Mathf.PI / 2f,
                Mathf.PI / 2f + 0.02f,
                Mathf.PI + 0.01f
            };

            var index = MathUtils.GetClosestLeftAngle(referenceAngle, angles);
            Assert.AreEqual(2, index);
        }
    }
}