using UnityEngine;

namespace game.character.movement
{
    public interface IMovement
    {
        public void PauseUntil(float time);

        public Direction GetDirection();
    }
}