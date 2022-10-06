using Assets.Scripts.game.character.characters.player;
using game.character.ability;
using game.character.ability.health;
using game.character.movement;
using game.character.player;
using game.character.state;
using game.Item;
using game.tool;
using UnityEngine;

namespace game.character.characters.player
{
    public class Player : MonoBehaviour, ICharacter
    {
        public CharacterType PlayerType { get; private set; }

        public StateHandler States { get; private set; }
        public AbilityHandler Abilities { get; }

        public PlayerStats Stats { get; private set; }

        private Health _health;

        public IWeapon Weapon;

        public ItemPickup ItemPickup;

        private bool _isActive;

        public bool IsActive { get => _isActive; set => _isActive = value; }

        public IMovement Movement { get; private set; }

        public void Construct(CharacterType playerType, PlayerStats stats)
        {
            PlayerType = playerType;
            States = new StateHandler();
            Stats = stats;
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
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (ItemPickup != null)
            {
                ItemPickup.Entered(collision.gameObject);
            }
        }

        public void AddDestroyable(GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }
    }
}