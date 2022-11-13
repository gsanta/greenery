using game.character.player;

namespace game.character.state
{
    public class IdleState : ICharacterState
    {
        private CharacterEvents _characterEvents;

        public IdleState(CharacterEvents characterEvents)
        {
            _characterEvents = characterEvents;
        }

        public CharacterStateType GetStateType()
        {
            return CharacterStateType.Idle;
        }

        public void StartState()
        {
        }

        public void EndState()
        {
        }
    }
}
