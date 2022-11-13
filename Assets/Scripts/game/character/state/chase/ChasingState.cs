using game.character.characters.enemy;
using game.character.characters.player;
using game.character.movement;
using game.character.player;
using UnityEngine;

namespace game.character.state.chase
{
    public class ChasingState : MonoBehaviour, ICharacterState
    {
        private const float TimerMax = 2.0f;

        private const CharacterStateType StateType = CharacterStateType.ChasingState;

        private Vector2 _targetPosition;
        
        private Enemy _enemy;
        
        private TargetMovementHandler _mover;

        private CharacterEvents _characterEvents;

        private PlayerStore _playerStore;
        
        public float targetTime = TimerMax;

        public void Construct(Enemy enemy, TargetMovementHandler mover, PlayerStore playerStore, CharacterEvents characterEvents)
        {
            _enemy = enemy;
            _mover = mover;
            _playerStore = playerStore;
            _characterEvents = characterEvents;
            
            _enemy.States.AddState(this);
        }

        public CharacterStateType GetStateType()
        {
            return StateType;
        }

        public void StartState()
        {
            UpdateTarget();

            if (_enemy.ShootingBehaviour != null && !_enemy.ShootingBehaviour.IsActive)
            {
                _enemy.ShootingBehaviour.IsActive = true;
            }

            _mover.MoveTo(_targetPosition);
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

            ICharacter minDistPlayer = null;
            float minDist = float.MaxValue;

            _playerStore.GetAll(PlayerType.Friend).ForEach(player =>
            {
                var newDist = Vector2.Distance(player.GetPosition(), _enemy.GetPosition());
                if (newDist < minDist)
                {
                    minDistPlayer = player;
                    minDist = newDist;
                }
            });

            _targetPosition = minDistPlayer.GetPosition();
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
            _mover.FinishMovement();
            _enemy.States.SetActiveState(CharacterStateType.RoamingState);
        }

        public void EndState()
        {
        }
    }
}