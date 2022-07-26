using AI.grid.path;
using Character;
using Character.player;
using UnityEngine;

namespace AI.state.character.states
{
    public class ChasingState : MonoBehaviour, ICharacterState
    {
        private const float TimerMax = 2.0f;

        private const CharacterStateType StateType = CharacterStateType.ChasingState;

        private Vector2 _targetPosition;
        
        private ICharacter _character;
        
        private PathMovement _pathMovement;

        private PlayerStore _playerStore;
        
        public float targetTime = TimerMax;

        public void Construct(ICharacter character, PathMovement pathMovement, PlayerStore playerStore)
        {
            _character = character;
            _pathMovement = pathMovement;
            _playerStore = playerStore;
            
            _character.StateHandler.AddState(this);
        }

        public CharacterStateType GetStateType()
        {
            return StateType;
        }

        public void StartState()
        {
            UpdateTarget();
        }

        public void UpdateState()
        {
            _pathMovement.MoveTo(_targetPosition);
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            targetTime -= Time.deltaTime;
 
            if (targetTime <= 0.0f)
            {
                UpdateTarget();
            }
        }

        private void UpdateTarget()
        {
            targetTime = TimerMax;
            _targetPosition = _playerStore.GetActivePlayer().GetPosition();
        }
    }
}