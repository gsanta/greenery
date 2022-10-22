using System.Collections.Generic;
using UnityEngine;

namespace game.character.movement
{
    public abstract class IMovement : MonoBehaviour
    {
        protected bool _isPaused;

        protected Direction _moveDirection = Direction.Down;

        protected ICharacter _character;

        public void Construct(ICharacter character)
        {
            _character = character;
        }

        public void PauseUntil(float time)
        {
            _isPaused = true;
            Invoke(nameof(Unpause), time);
        }

        private void Unpause()
        {
            _isPaused = false;
        }

        public Direction GetDirection()
        {
            return _moveDirection;
        }

        public List<Vector2> GetPath()
        {
            return new List<Vector2>();
        }
    }
}