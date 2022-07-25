namespace AI.state.character
{
    public interface ICharacterState
    {
        public CharacterStateType GetStateType();
        public void StartState();
        public void UpdateState();
    }
}