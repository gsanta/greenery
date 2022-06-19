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
        private Vector2 _anchorPoint;
        private Vector2 _currentPoint;
        private Vector2 _prevCurrentPoint;
        private readonly List<Vector2> _points;
        private List<Vector2> _activePoints;
        private List<float> _angles;
        private List<float> _angleDistances;
        private readonly List<Segment> _segments = new List<Segment>();
        private Segment _activeSegment;
        private Vector2? _prevLeftNeighbour;
        private Vector2? _prevRightNeighbour;
        private Side _leftRightEqualWhichSide = Side.Left;
        
        public RopeWrapper(Vector2 startPoint, List<Vector2> points)
        {
            _anchorPoint = startPoint;
            this._points = points;
        }

        public List<Segment> GetSegments()
        {
            return _segments;
        }

        public List<Vector2> GetPoints()
        {
            var points = new List<Vector2>();

            if (_segments.Count > 0)
            {
                points.Add((_segments[0].start));
            }
            
            _segments.ForEach((segment) =>
            {
                points.Add(segment.end);
            });
            
            points.Add(_currentPoint);

            return points;
        }
        
        public void Update(Vector2 currentPoint)
        {
            this._currentPoint = currentPoint;
            _activeSegment = new Segment(_anchorPoint, currentPoint);
            
            _activePoints = _points.Where((point) => MathUtils.VectorDistance(_anchorPoint, point) <= _activeSegment.GetLength()).ToList();
            var neighbours = GetNeighbours(_activePoints, _anchorPoint, currentPoint);

            // var (leftNeighbour, rightNeighbour) = GetNeighbours(_activePoints, _anchorPoint, currentPoint);

            if (neighbours.HasValue)
            {
                var (leftNeighbour, rightNeighbour) = neighbours.Value;
                if (_prevLeftNeighbour == null)
                {
                    _prevLeftNeighbour = leftNeighbour;
                    _prevRightNeighbour = rightNeighbour;
                    _prevCurrentPoint = currentPoint;
                    return;
                }

                if (!_activePoints.Contains(_prevLeftNeighbour.Value))
                {
                    _prevLeftNeighbour = leftNeighbour;
                }

                if (!_activePoints.Contains(_prevRightNeighbour.Value))
                {
                    _prevRightNeighbour = rightNeighbour;
                }

                if (HasSideChanged(leftNeighbour, currentPoint, _prevCurrentPoint, _anchorPoint))
                {
                    _segments.Add(new Segment(_anchorPoint, leftNeighbour));
                    _anchorPoint = leftNeighbour;
                }
                else if (HasSideChanged(rightNeighbour, currentPoint, _prevCurrentPoint, _anchorPoint))
                {
                    _segments.Add(new Segment(_anchorPoint, rightNeighbour));
                    _anchorPoint = rightNeighbour;
                }
            }

            _prevCurrentPoint = currentPoint;
        }

        public static (Vector2, Vector2)? GetNeighbours(List<Vector2> points, Vector2 anchor, Vector2 curr)
        {
            if (points.Count == 0)
            {
                return null;
            }
            var segment = new Segment(anchor, curr);
            var angles = points.Select((point) => MathUtils.GetAngle(anchor, point)).ToList();
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

        public static bool HasSideChanged(Vector2 targetPoint, Vector2 currentPoint, Vector2 prevCurrentPoint, Vector2 anchorPoint)
        {
            var angleCurr = MathUtils.GetAngle(anchorPoint, currentPoint);
            var anglePrevCurr = MathUtils.GetAngle(anchorPoint, prevCurrentPoint);
            var angleTarget = MathUtils.GetAngle(anchorPoint, targetPoint);

            var distCurr = MathUtils.GetAngleDistance(angleCurr, angleTarget);
            var distPrevCurr = MathUtils.GetAngleDistance(anglePrevCurr, angleTarget);

            var SIDE_CHANGE_THRESHOLD = Math.PI;
            if (Mathf.Abs(distCurr - distPrevCurr) > SIDE_CHANGE_THRESHOLD)
            {
                return true;
            }

            return false;
        }
    }
}