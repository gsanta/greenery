using Characters;
using Grid;
using UnityEngine;

namespace AI.Player
{
    public class SteeringState
    {
        public IntPosition BaseGridPosition { set; get; }
     
        private IntPosition _targetGridPosition;
        
        private Vector2 _targetPosition;

        private Grid<PathNode> _grid;
        
        private readonly IMoveAble _character;

        private const float TargetThreshold = 0.2f;

        public SteeringState(Grid<PathNode> grid, IMoveAble character)
        {
            _grid = grid;
            _character = character;
        }
        
        public void UpdateState()
        {
            if (IsTargetReached())
            {
                FindNewTarget();
                _character.SetTarget(_targetPosition);
            }
        }

        private void FindNewTarget()
        {
            BaseGridPosition = _targetGridPosition;
            _targetGridPosition = new IntPosition(BaseGridPosition.X + 1, BaseGridPosition.Y);
            _targetPosition = _grid.GetWorldPosition(_targetGridPosition.X, _targetGridPosition.Y);
        }

        private bool IsTargetReached()
        {
            return Vector2.Distance(_character.GetPosition(), _targetPosition) < TargetThreshold;
        }
    }
}