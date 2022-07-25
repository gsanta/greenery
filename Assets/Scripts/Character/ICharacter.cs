using Characters.Common;
using UnityEngine;

namespace Characters
{
    public interface ICharacter
    {
        Direction GetMoveDirection();

        Health GetHealth();

        void Die();
    }
}