using Algorithms;
using UnityEngine;

namespace Types
{
    public struct Segment
    {
        public Vector2 start;
        public Vector2 end;

        public Segment(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public float GetLength()
        {
            return (start - end).magnitude;
        }

        public float GetAngle()
        {
            return MathUtils.GetAngle(start, end);
        }
    }
}