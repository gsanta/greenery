using AI.GridSystem;
using AI.pathFinding;
using Characters;
using UnityEngine;

namespace AI.Player
{
    public class RoamingState
    {

        private Vector2 _startingPosition;
        private Vector2 _roamPosition;
        private Grid<PathNode> _grid;
        private readonly IMoveAble _moveAble;
        private readonly PathMovement _pathMovement;
        
        public RoamingState(Grid<PathNode> grid, IMoveAble moveAble, PathMovement pathMovement)
        {
            _grid = grid;
            _moveAble = moveAble;
            _pathMovement = pathMovement;
        }

        public void Start()
        {
            _startingPosition = _moveAble.GetPosition();
            _roamPosition = GetRoamingPosition();
        }

        public void UpdateState()
        {
            _pathMovement.MoveTo(_roamPosition);
            const float reachedPositionDistance = 1f;
            if (Vector2.Distance(_moveAble.GetPosition(), _roamPosition) < reachedPositionDistance)
            {
                _roamPosition = GetRoamingPosition();
            }
        }

        private Vector2 GetRoamingPosition()
        {
            return _startingPosition + Utilities.GetRandomDir() * Random.Range(5f, 20f);
        }
    }
}