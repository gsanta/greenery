using AI.state.character;
using Character.ability;
using Character.state;
using Characters.Common;
using UnityEngine;

namespace Character
{
    public interface ICharacter
    {
        Direction GetMoveDirection();
        
        StateHandler States { get; }
        
        AbilityHandler Abilities { get; }

        Health GetHealth();

        void Die();
        
        Vector2 GetPosition();
        
        void SetMovement(Vector2 movement);
        
        Vector2 GetMovement();
    }
}