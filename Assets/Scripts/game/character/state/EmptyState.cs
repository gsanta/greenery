using game.character.player;

namespace game.character.state
{
    public class EmptyState : ICharacterState
    {
        private CharacterEvents _characterEvents;

        public EmptyState(CharacterEvents characterEvents)
        {
            _characterEvents = characterEvents;
        }

        public CharacterStateType GetStateType()
        {
            return CharacterStateType.Empty;
        }

        public void StartState()
        {
        }

        public void EndState()
        {
        }
    }
}
