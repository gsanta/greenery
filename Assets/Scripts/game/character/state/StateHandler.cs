using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace game.character.state
{
    public class StateHandler
    {
        private readonly List<ICharacterState> _states = new();
        [CanBeNull] public ICharacterState ActiveState { get; private set; }
        
        public void AddState(ICharacterState state)
        {
            _states.Add(state);
        }

        public void SetActiveState(CharacterStateType type)
        {
            var state = _states.Find((element) => element.GetStateType() == type);

            ActiveState = state ?? throw new InvalidOperationException("State not found: " + type);
            ActiveState.StartState();
        }
    }
}