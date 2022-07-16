using Characters.Common;
using UnityEngine;

namespace Characters
{
    public interface ICharacter
    {
        Direction GetMoveDirection();

        Vector3 GetPosition();

        Health GetHealth();

        void Die();
    }
}