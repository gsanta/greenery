using System;
using AI.GridSystem;
using Characters;
using UnityEngine;

namespace AI.Player
{
    public class SteeringState
    {
        private IntPosition BaseGridPosition { set; get; }
     
        private IntPosition _targetGridPosition;
        
        private Vector2 _targetPosition;

        private readonly Grid<PathNode> _grid;
        
        private readonly IMoveAble _moveAble;

        private const float TargetThreshold = 0.2f;

        private Func<Direction> _findDirection;

        public SteeringState(Grid<PathNode> grid, IMoveAble moveAble, Func<Direction> findDirection)
        {
            _grid = grid;
            _moveAble = moveAble;
            _findDirection = findDirection;
        }

        public void Activate()
        {
            var startGridPosition = new IntPosition(_grid.GetGridPosition(_moveAble.GetPosition()));
            FindNewTarget(startGridPosition);
            SetMovement();
        }
        
        public void Update()
        {
            if (IsTargetReached())
            {
                FindNewTarget(_targetGridPosition);
            }

            SetMovement();
        }

        private void SetMovement()
        {
            var movement = _targetPosition - _moveAble.GetPosition();
            movement.Normalize();
            _moveAble.SetMovement(movement);
        }

        private void FindNewTarget(IntPosition basePosition)
        {
            BaseGridPosition = basePosition;
            _targetGridPosition = new IntPosition(BaseGridPosition.X + 1, BaseGridPosition.Y);
            _targetPosition = _grid.GetWorldPosition(_targetGridPosition.X, _targetGridPosition.Y);
        }

        private bool IsTargetReached()
        {
            return Vector2.Distance(_moveAble.GetPosition(), _targetPosition) < TargetThreshold;
        }
    }
}