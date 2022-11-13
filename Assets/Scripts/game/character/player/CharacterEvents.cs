
using System;

namespace game.character.player
{
    public class OnGridChangeEventArgs : EventArgs
    {
        public ICharacter character;

        public PathNode from;

        public PathNode to;

        public OnGridChangeEventArgs(ICharacter character, PathNode from, PathNode to)
        {
            this.character = character;
            this.from = from;
            this.to = to;
        }
    }

    public class CharacterEvents
    {

        public event EventHandler OnTargetEnd;

        public event EventHandler OnTargetStart;

        public event EventHandler<OnGridChangeEventArgs> OnGridChange;

        public void EmitTargetEnd()
        {
            OnTargetEnd?.Invoke(this, EventArgs.Empty);
        }

        public void EmitGridChange(ICharacter character, PathNode from, PathNode to)
        {
            OnGridChange?.Invoke(this, new OnGridChangeEventArgs(character, from, to));
        }
    }
}
