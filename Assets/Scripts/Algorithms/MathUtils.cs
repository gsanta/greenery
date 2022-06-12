using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Algorithms
{
    public class MathUtils : MonoBehaviour
    {
        public static float GetAngle(Vector2 start, Vector2 end)
        {
            var x = end.x - start.x;
            var y = end.y - start.y;
            var result = Mathf.Atan2(y, x);

            return result >= 0 ? result : result + Mathf.PI * 2;
        }

        public static float GetAngleDistance(float angle1, float angle2)
        {
            if (angle2 <= angle1)
            {
                return angle1 - angle2;
            }
            else
            {
                return 2 * Mathf.PI - (angle2 - angle1);
            }
        }

        public static int GetClosestLeftAngle(float referenceAngle, List<float> angles)
        {
            var angleDeltas = angles .Select(angle => GetAngleDistance(referenceAngle, angle)).ToList();
                
            var min = angleDeltas.Min();

            return angleDeltas.IndexOf(min);
        }

        public static float VectorDistance(Vector2 vec1, Vector2 vec2)
        {
            return (vec1 - vec2).magnitude;
        }
    }
}