using game.character.movement;
using game.character.player;
using game.Item;
using UnityEngine;

namespace game.character.characters.player
{
    public class Player : ICharacter
    {
        public CharacterType PlayerType { get; private set; }

        public PlayerStats Stats { get; private set; }

        public ItemPickup ItemPickup;

        public void Construct(CharacterType playerType, PlayerStats stats)
        {
            base.Construct(new WeaponHolder());
            PlayerType = playerType;
            Stats = stats;
        }

        void Start()
        {
            Movement = GetComponent<Movement>();
        }
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (ItemPickup != null)
            {
                ItemPickup.Entered(collision.gameObject);
            }
        }       
    }
}