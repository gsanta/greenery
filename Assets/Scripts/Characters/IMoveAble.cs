using UnityEngine;

namespace Characters
{
    public interface IMoveAble
    {
        Vector2 GetPosition();
        void SetMovement(Vector2 movement);
        Vector2 GetMovement();
    }
}