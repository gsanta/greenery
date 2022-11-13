

using game.scene.grid.path;
using UnityEngine;
using System.Collections.Generic;
using game.scene.level;
using game.character.player;
using game.Common;

namespace game.character.movement
{
    public class TargetMovementHandler : MovementHandler
    {
        private List<Vector2> _pathVectorList = new();

        private int _currentPathIndex = 0;

        private PathFinding _pathFinding = new PathFinding();

        private Vector2 _targetPosition;

        private Level _level;

        private ICharacter _character;

        private CharacterEvents _characterEvents;

        public bool _isTargetReached { get; private set; } = false;

        public TargetMovementHandler(ICharacter character, Level level, CharacterEvents characterEvents)
        {
            _character = character;
            _level = level;
            _characterEvents = characterEvents;
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public void MovementFinished()
        {
            _currentPathIndex++;
            if (_currentPathIndex >= _pathVectorList.Count)
            {
                FinishMovement();
            }
            else
            {
                UpdateTarget();
            }
        }

        public void MoveTo(Vector2 targetPosition)
        {
            if (_targetPosition != targetPosition || _pathVectorList == null)
            {
                FinishMovement();
                SetTargetPosition(targetPosition);
            }
        }

        public void FinishMovement()
        {
            _isTargetReached = true;
            _pathVectorList = null;

            _characterEvents.EmitTargetEnd();
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

            var position = _character.GetPosition();
            var targetPosition = _pathVectorList[_currentPathIndex];

            var _direction = (targetPosition - position).normalized;
            movement.SetDirection(_direction);
            movement.SetDestination(targetPosition);
            movement.IsTargetReached = false;
        }
    }
}
