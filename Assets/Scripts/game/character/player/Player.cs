using Assets.Scripts.game.character.characters.player;
using game.character.ability;
using game.character.ability.health;
using game.character.movement;
using game.character.player;
using game.character.state;
using game.character.utils;
using game.tool;
using UnityEngine;

namespace game.character.characters.player
{
    public class Player : MonoBehaviour, ICharacter
    {
        public PlayerType PlayerType { get; private set; }

        public StateHandler States { get; private set; }
        public AbilityHandler Abilities { get; }

        private Health _health;

        public ITool Weapon;

        private bool _isActive;

        public bool IsActive { get => _isActive; set => _isActive = value; }

        public IMovement Movement { get; private set; }

        public void Construct(PlayerType playerType)
        {
            PlayerType = playerType;
            States = new StateHandler();
        }

        void Start()
        {
            _health = GetComponent<Health>();
            Movement = GetComponent<InputMovement>();
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void SetMovement(Vector2 movement)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetMovement()
        {
            throw new System.NotImplementedException();
        }

        public Health GetHealth()
        {
            return _health;
        }
        
        public void Die()
        {
            Debug.Log("player is dead");
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}