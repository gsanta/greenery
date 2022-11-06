
using System;

namespace game.character.player
{
    public class CharacterEvents
    {

        public event EventHandler OnTargetEnd;

        public event EventHandler OnTargetStart;

        public void EmitTargetEnd()
        {
            OnTargetEnd?.Invoke(this, EventArgs.Empty);
        }
        
        public void EmitTargetStart()
        {
            OnTargetStart?.Invoke(this, EventArgs.Empty);
        }
    }
}
