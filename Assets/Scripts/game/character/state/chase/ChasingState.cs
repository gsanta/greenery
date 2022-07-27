using game.character.ability;
using game.character.characters.player;
using game.scene.grid.path;
using UnityEngine;

namespace game.character.state.chase
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
            
            _character.States.AddState(this);
        }

        public CharacterStateType GetStateType()
        {
            return StateType;
        }

        public void StartState()
        {
            UpdateTarget();
            _character.Abilities.Get(AbilityType.Shoot).IsActive = true;
        }
        
        public void UpdateState()
        {
            _pathMovement.MoveTo(_targetPosition);
            UpdateTimer();
            if (CheckFinishState())
            {
                FinishState();
            }
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
        
        private bool CheckFinishState()
        {
            const float targetRange = 10f;
            var player = _playerStore.GetActivePlayer();
            return Vector2.Distance(_character.GetPosition(), player.GetPosition()) > targetRange;
        }

        private void FinishState()
        {
            targetTime = TimerMax;
            _character.Abilities.Get(AbilityType.Shoot).IsActive = false;
            _character.States.SetActiveState(CharacterStateType.RoamingState);
        }
    }
}