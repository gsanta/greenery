using Character.feature.rope;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Algorithms.RopeWrapping
{
    public class BezierHelperTest
    {
        [Test]
        public void TestBezier()
        {
            var point1 = new Vector2(0, 0);
            var point2 = new Vector2(6, 8);
            var point3 = new Vector2(12, 0);
            var bezier = BezierHelper.CreateBezier(point1, point2, point3);
            var a = 2;
        }
        
    }
}