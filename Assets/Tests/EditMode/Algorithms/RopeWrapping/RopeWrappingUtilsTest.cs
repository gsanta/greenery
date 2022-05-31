using Algorithms.RopeWrapping;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Algorithms.RopeWrapping
{
    public class RopeWrappingUtilsTest
    {
        [Test]
        public void GetRotationPI()
        {
            var rotation = RopeWrappingUtils.GetRotation(new Vector2(0f, 0f), new Vector2(-1f, 0));
            Assert.AreEqual(Mathf.PI, rotation);
        }
        
        [Test]
        public void GetRotationPIOver2()
        {
            var rotation = RopeWrappingUtils.GetRotation(new Vector2(0f, 0f), new Vector2(0f, 1f));
            Assert.AreEqual(Mathf.PI / 2, rotation);
        }
        
        [Test]
        public void GetRotation3PIOver2()
        {
            var rotation = RopeWrappingUtils.GetRotation(new Vector2(0f, 0f), new Vector2(0f, -1f));
            Assert.AreEqual(3 * Mathf.PI / 2, rotation);
        }
    }
}