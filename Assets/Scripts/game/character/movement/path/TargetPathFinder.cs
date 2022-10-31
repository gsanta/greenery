

using game.scene.grid.path;
using game.scene.grid;
using UnityEngine;
using System.Collections.Generic;
using game.character.movement.path;

namespace game.character.movement
{
    public class TargetPathFinder : MonoBehaviour
    {
        private List<Vector2> _pathVectorList = new();

        private int _currentPathIndex = 0;

        private PathFinding _pathFinding;

        private GridGraph _gridGraph;

        private Vector2 _targetPosition;

        private MovementPath _movementPath;

        public bool _isTargetReached { get; private set; }

        public void Construct(GridGraph gridGraph, MovementPath movementPath)
        {
            _pathFinding = new PathFinding();
            _isTargetReached = false;
            _gridGraph = gridGraph;
            _movementPath = movementPath;
        }

        public void MoveTo(Vector2 targetPosition)
        {
            if (_targetPosition != targetPosition || _pathVectorList == null)
            {
                FinishMovement();
                SetTargetPosition(targetPosition);
            }
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_pathVectorList == null || _pathVectorList.Count == 0)
            {
                FinishMovement();
                return;
            }

            if (_movementPath.IsTargetReached)
            {
                _currentPathIndex++;
                if (_currentPathIndex >= _pathVectorList.Count)
                {
                    FinishMovement();
                } else
                {
                    UpdateTarget();
                }
            }
        }

        public void FinishMovement()
        {
            _isTargetReached = true;
            _pathVectorList = null;
        }

        private void SetTargetPosition(Vector2 targetPosition)
        {
            
            _isTargetReached = false;
            _targetPosition = targetPosition;
            _currentPathIndex = 0;
            var pathNodeList = new List<PathNode>();
            _pathVectorList = _pathFinding.FindPath(_gridGraph, transform.position, targetPosition, out pathNodeList);


            if (_pathVectorList is { Count: > 1 })
            {
                _pathVectorList.RemoveAt(0);
            }

            UpdateTarget();
        }

        private void UpdateTarget()
        {
            var position = (Vector2)transform.position;
            var targetPosition = _pathVectorList[_currentPathIndex];

            var _direction = (targetPosition - position).normalized;
            _movementPath.SetDirection(_direction);
            _movementPath.SetDestination(targetPosition);
            _movementPath.IsTargetReached = false;
        }
    }
}
