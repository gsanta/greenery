using game.character.movement.path;
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

        private bool _isCurrentPlayer;

        public bool IsCurrentPlayer {
            get => _isCurrentPlayer;
            set
            {
                _isCurrentPlayer = value;
            } 
        }

        public void Construct(CharacterType playerType, PlayerStats stats, Movement movementPath)
        {
            base.Construct();
            PlayerType = playerType;
            Stats = stats;
            Movement = movementPath;
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