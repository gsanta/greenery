using game.character.player;

namespace game.character.state
{
    public class PassThroughState : ICharacterState
    {
        private CharacterEvents _characterEvents;

        public PassThroughState(CharacterEvents characterEvents)
        {
            _characterEvents = characterEvents;
        }

        public CharacterStateType GetStateType()
        {
            return CharacterStateType.PassThrough;
        }

        public void ActivateState()
        {
            _characterEvents.EmitTargetEnd();
        }

        public void DeActivateState()
        {
        }
    }
}
