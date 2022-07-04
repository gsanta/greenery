using Characters.Common;
using UnityEngine;

namespace Characters
{
    public interface ICharacter
    {
        Direction GetMoveDirection();

        Vector3 GetPosition();

        GameObject GetGameObjet();

        Health GetHealth();

        void Die();
    }
}