using UnityEngine;

namespace Algorithms.RopeWrapping
{
    public class RopeWrappingUtils : MonoBehaviour
    {
        public static float GetRotation(Vector2 start, Vector2 end)
        {
            var x = end.x - start.x;
            var y = end.y - start.y;
            var result = Mathf.Atan2(y, x);

            return result >= 0 ? result : result + Mathf.PI * 2;
        }
    }
}