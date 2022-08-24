using game.character.characters.enemy;
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
        
        private Enemy _enemy;
        
        private PathMovement _pathMovement;

        private PlayerStore _playerStore;
        
        public float targetTime = TimerMax;

        public void Construct(Enemy enemy, PathMovement pathMovement, PlayerStore playerStore)
        {
            _enemy = enemy;
            _pathMovement = pathMovement;
            _playerStore = playerStore;
            
            _enemy.States.AddState(this);
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
            if (_enemy.ShootingBehaviour != null && !_enemy.ShootingBehaviour.IsActive)
            {
                _enemy.ShootingBehaviour.IsActive = true;
            }

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
            return false;
            //const float targetRange = 10f;
            //var player = _playerStore.GetActivePlayer();
            //return Vector2.Distance(_enemy.GetPosition(), player.GetPosition()) > targetRange;
        }

        private void FinishState()
        {
            targetTime = TimerMax;
            _enemy.ShootingBehaviour.IsActive = true;
            _pathMovement.FinishMovement();
            _enemy.States.SetActiveState(CharacterStateType.RoamingState);
        }
    }
}