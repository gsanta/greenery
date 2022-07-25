using AI.grid.path;
using Characters;
using UnityEngine;

namespace AI.state.character.states
{
    public class ChasingState : MonoBehaviour, ICharacterState
    {
        private const CharacterStateType StateType = CharacterStateType.ChasingState;

        private Vector2 _startingPosition;
        
        private Vector2 _roamPosition;
        
        private readonly IMoveAble _moveAble;
        
        private readonly PathMovement _pathMovement;

        private readonly StateHandler _stateHandler;

        public ChasingState(StateHandler stateHandler, IMoveAble moveAble, PathMovement pathMovement)
        {
            _stateHandler = stateHandler;
            _moveAble = moveAble;
            _pathMovement = pathMovement;
            
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
        }
    }
}