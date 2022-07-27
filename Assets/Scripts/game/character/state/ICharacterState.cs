namespace game.character.state
{
    public interface ICharacterState
    {
        public CharacterStateType GetStateType();
        
        public void StartState();
        
        public void UpdateState();
    }
}