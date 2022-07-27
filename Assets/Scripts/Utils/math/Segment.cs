using System;
using UnityEngine;

namespace Utils.math
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

        public bool Equals(Segment other)
        {
            return start.Equals(other.start) && end.Equals(other.end);
        }

        public override bool Equals(object obj)
        {
            return obj is Segment other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(start, end);
        }
    }
}