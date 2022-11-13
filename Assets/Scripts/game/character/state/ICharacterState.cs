namespace game.character.state
{
    public interface ICharacterState
    {
        public CharacterStateType GetStateType();
        
        public void ActivateState();

        public void DeActivateState();
    }
}