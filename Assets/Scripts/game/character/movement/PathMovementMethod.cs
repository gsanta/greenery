

using game.scene.grid.path;
using game.scene.grid;
using UnityEngine;
using System.Collections.Generic;

namespace game.character.movement
{
    public class PathMovementMethod : MonoBehaviour, MovementMethod
    {
        private List<Vector2> _pathVectorList = new();

        private int _currentPathIndex = 0;

        private PathFinding _pathFinding;

        private GridGraph _gridGraph;

        private Vector2 _targetPosition;

        private ICharacter _character;

        public bool _isTargetReached { get; private set; }

        public void Construct(ICharacter character, GridGraph gridGraph)
        {
            _character = character;
            _pathFinding = new PathFinding();
            _isTargetReached = false;
            _gridGraph = gridGraph;
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

            var position = (Vector2)transform.position;
            var targetPosition = _pathVectorList[_currentPathIndex];
            if (Vector2.Distance(position, targetPosition) > 0.2f)
            {
                var _direction = (_targetPosition - position).normalized;
                _character.Movement.SetDirection(_direction);
            }
            else
            {
                _currentPathIndex++;
                if (_currentPathIndex >= _pathVectorList.Count)
                {
                    FinishMovement();
                }
            }

            _character.Movement.SetIsMoving(!_isTargetReached);
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
        }
    }
}
