using UnityEngine;

namespace game.Common
{
    public interface MovementHandler
    {
        void Activate();

        void Deactivate();

        void MovementFinished();

        void MoveTo(Vector2 targetPosition);
    }
}
