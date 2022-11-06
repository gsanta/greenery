

using game.scene.grid.path;
using UnityEngine;
using System.Collections.Generic;
using game.scene.level;

namespace game.character.movement
{
    public class TargetPathFinder : MonoBehaviour
    {
        private List<Vector2> _pathVectorList = new();

        private int _currentPathIndex = 0;

        private PathFinding _pathFinding = new PathFinding();

        private Vector2 _targetPosition;

        private Level _level;

        private ICharacter _character;

        public bool _isTargetReached { get; private set; } = false;

        public void SetLevel(Level level)
        {
            _level = level;
        }

        public void SetCharacter(ICharacter character)
        {
            _character = character;
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

            if (_character.Movement.IsTargetReached)
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
            _pathVectorList = _pathFinding.FindPath(_level.Grid, _character.GetPosition(), targetPosition, out pathNodeList);


            if (_pathVectorList is { Count: > 1 })
            {
                _pathVectorList.RemoveAt(0);
            }

            UpdateTarget();
        }

        private void UpdateTarget()
        {
            var movement = _character.Movement;

            var position = (Vector2)transform.position;
            var targetPosition = _pathVectorList[_currentPathIndex];

            var _direction = (targetPosition - position).normalized;
            movement.SetDirection(_direction);
            movement.SetDestination(targetPosition);
            movement.IsTargetReached = false;
        }
    }
}
