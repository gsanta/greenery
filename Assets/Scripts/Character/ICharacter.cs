using AI.state.character;
using Characters.Common;
using UnityEngine;

namespace Character
{
    public interface ICharacter
    {
        Direction GetMoveDirection();
        
        StateHandler StateHandler { get; }

        Health GetHealth();

        void Die();
        
        Vector2 GetPosition();
        
        void SetMovement(Vector2 movement);
        
        Vector2 GetMovement();
    }
}