using System.Collections.Generic;
using UnityEngine;

namespace game.character.movement
{
    public interface IMovement
    {
        public void PauseUntil(float time);

        public Direction GetDirection();

        public List<Vector2> GetPath();
    }
}