using AI.grid.path;
using Character.player;
using Characters;
using UnityEngine;

namespace AI.state.character.states
{
    public class RoamingState : ICharacterState
    {
        private const CharacterStateType StateType = CharacterStateType.RoamingState;

        private Vector2 _startingPosition;
        
        private Vector2 _roamPosition;
        
        private readonly IMoveAble _moveAble;
        
        private readonly PathMovement _pathMovement;
        
        private PlayerStore _playerStore;

        private readonly StateHandler _stateHandler;
        
        public RoamingState(StateHandler stateHandler, IMoveAble moveAble, PathMovement pathMovement, PlayerStore playerStore)
        {
            _moveAble = moveAble;
            _pathMovement = pathMovement;
            _playerStore = playerStore;

            _stateHandler = stateHandler;
            _stateHandler.AddState(this);
        }
        
        public CharacterStateType GetStateType()
        {
            return StateType;
        }

        public void StartState()
        {
            _startingPosition = _moveAble.GetPosition();
            _roamPosition = GetRoamingPosition();
        }

        public void UpdateState()
        {
            _pathMovement.MoveTo(_roamPosition);
            if (_pathMovement.IsTargetReached)
            {
                _roamPosition = GetRoamingPosition();
            }
        }

        private Vector2 GetRoamingPosition()
        {
            return _startingPosition + Utilities.GetRandomDir() * Random.Range(5f, 5f);
        }

        private void CheckTarget()
        {
            const float targetRange = 50f;
            var player = _playerStore.GetActivePlayer();
            if (Vector2.Distance(_moveAble.GetPosition(), player.GetPosition()) < targetRange)
            {
                _stateHandler.SetActiveState(CharacterStateType.ChasingState);
            }
        }
    }
}