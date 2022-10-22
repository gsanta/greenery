using Assets.Scripts.game.character.characters.player;
using game.character.player;
using game.Item;
using game.tool;
using UnityEngine;

namespace game.character.characters.player
{
    public class Player : ICharacter
    {
        public CharacterType PlayerType { get; private set; }

        public PlayerStats Stats { get; private set; }

        public IWeapon Weapon;

        public ItemPickup ItemPickup;

        public void Construct(CharacterType playerType, PlayerStats stats)
        {
            base.Construct();
            PlayerType = playerType;
            Stats = stats;
        }

        void Start()
        {
            Movement = GetComponent<InputMovement>();
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