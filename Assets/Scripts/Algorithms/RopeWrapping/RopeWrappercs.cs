using System;
using System.Collections.Generic;
using System.Linq;
using Types;
using UnityEngine;

namespace Algorithms.RopeWrapping
{
    enum Side {
        Left,
        Right
    }
    
    public class RopeWrapper
    {
        private Vector2 anchorPoint;
        private Vector2 currentPoint;
        private List<Vector2> points;
        private List<Vector2> activePoints;
        private List<float> angles;
        private List<float> angleDistances;
        private List<Segment> segments = new List<Segment>();
        private Segment activeSegment;
        private Nullable<Vector2> prevLeftNeighbour;
        private Nullable<Vector2> prevRightNeighbour;
        private Side leftRightEqualWhichSide = Side.Left;
        
        public RopeWrapper(Vector2 startPoint, List<Vector2> points)
        {
            anchorPoint = startPoint;
            this.points = points;
        }
        
        public void Update(Vector2 currentPoint)
        {
            this.currentPoint = currentPoint;
            activeSegment = new Segment(anchorPoint, currentPoint);
        }

        private void SetActivePoints()
        {
            activePoints = points.Where((point) => MathUtils.VectorDistance(anchorPoint, point) <= activeSegment.GetLength()).ToList();
            var (leftNeighbour, rightNeighbour) = GetNeighbours(activePoints, anchorPoint, currentPoint);

            if (prevLeftNeighbour == null)
            {
                prevLeftNeighbour = leftNeighbour;
                prevRightNeighbour = rightNeighbour;
                return;
            }

            if (!activePoints.Contains(prevLeftNeighbour.Value))
            {
                prevLeftNeighbour = leftNeighbour;
            }

            if (!activePoints.Contains(prevRightNeighbour.Value))
            {
                prevRightNeighbour = rightNeighbour;
            }
        }

        public static (Vector2, Vector2) GetNeighbours(List<Vector2> points, Vector2 anchor, Vector2 curr)
        {
            var segment = new Segment(anchor, curr);
            var angles = points.Select((point) => MathUtils.GetAngle(curr, point)).ToList();
            var angleDistances = angles.Select((angle) => MathUtils.GetAngleDistance(segment.GetAngle(), angle)).ToList();
            var leftIndex = FindMinIndex(angleDistances);
            var rightIndex = FindMaxIndex(angleDistances);

            return (points[leftIndex], points[rightIndex]);
        }

        private static int FindMinIndex(List<float> list)
        {
            int pos = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < list[pos]) { pos = i; }
            }

            return pos;
        }
        
        private static int FindMaxIndex(List<float> list)
        {
            int pos = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] >= list[pos]) { pos = i; }
            }

            return pos;
        }

        private static bool HasSideChanged(Side prevSide, Vector2 prevPoint, Vector2 anchorPoint, Vector2 currentPoint)
        {
                        
        }
        
        private static bool GetSide(Vector2 point, Vector2 anchorPoint)
    }
}