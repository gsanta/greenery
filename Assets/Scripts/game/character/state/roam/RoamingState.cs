using game.character.characters.player;
using game.character.movement;
using UnityEngine;

namespace game.character.state.roam
{
    public class RoamingState : ICharacterState
    {
        private const CharacterStateType StateType = CharacterStateType.RoamingState;

        private Vector2 _startingPosition;
        
        private Vector2 _roamPosition;
        
        private readonly ICharacter _character;
        
        private readonly TargetMovementHandler _pathMovementMethod;
        
        private PlayerStore _playerStore;

        public RoamingState(ICharacter character, TargetMovementHandler pathMovementMethod, PlayerStore playerStore)
        {
            _character = character;
            _pathMovementMethod = pathMovementMethod;
            _playerStore = playerStore;

            character.States.AddState(this);
        }
        
        public CharacterStateType GetStateType()
        {
            return StateType;
        }

        public void ActivateState()
        {
            _startingPosition = _character.GetPosition();
            _roamPosition = GetRoamingPosition();
        }

        private Vector2 GetRoamingPosition()
        {
            return _startingPosition + Utilities.GetRandomDir() * Random.Range(5f, 5f);
        }

        private void CheckTarget()
        {
            const float targetRange = 5f;
            var player = _playerStore.GetCurrentPlayer();
            if (Vector2.Distance(_character.GetPosition(), player.GetPosition()) < targetRange)
            {
                _pathMovementMethod.ClearState();
                _character.States.SetActiveState(CharacterStateType.ChasingState);
            }
        }

        public void DeActivateState()
        {
        }
    }
}