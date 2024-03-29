using game.character.movement.path;
using game.character.player;
using game.Common;
using game.Item;
using game.scene.grid;
using UnityEngine;

namespace game.character.characters.player
{
    public class Player : ICharacter
    {
        public PlayerStats Stats { get; private set; }

        public ItemPickup ItemPickup;

        public void Construct(CharacterType playerType, PlayerStats stats, Movement movementPath, GridGraph grid, CharacterEvents characterEvents, MovementHandler movementHandler)
        {
            base.Construct(PlayerType.Friend, grid, characterEvents, movementHandler);
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