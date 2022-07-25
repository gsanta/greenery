using System;
using System.Collections.Generic;

namespace AI.state.character
{
    public class StateHandler
    {
        private readonly List<ICharacterState> _states = new();
        private ICharacterState _activeState;
        
        public void AddState(ICharacterState state)
        {
            _states.Add(state);
        }

        public void SetActiveState(CharacterStateType type)
        {
            var state = _states.Find((element) => element.GetStateType() == type);

            _activeState = state ?? throw new InvalidOperationException("State not found: " + type);
            _activeState.StartState();
        }
    }
}