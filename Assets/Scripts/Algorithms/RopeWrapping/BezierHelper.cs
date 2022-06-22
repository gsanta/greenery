using System.Geometry;
using UnityEngine;

namespace Algorithms.RopeWrapping
{
    public class BezierHelper
    {

        public static Bezier CreateBezier(Vector2 point1, Vector2 point2, Vector2 point3)
        {

            var p1 = new System.Numerics.Vector2(point1.x, point1.y);
            var p2 = new System.Numerics.Vector2(point2.x, point2.y);
            var p3 = new System.Numerics.Vector2(point3.x, point3.y);
            
            return new Bezier(p1, p2, p3);
        }
    }
}