using game.character.ability;
using game.character.ability.health;
using game.character.movement;
using game.character.state;
using UnityEngine;

namespace game.character
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

        public IMovement Movement { get; }

        GameObject GetGameObject();
    }
}