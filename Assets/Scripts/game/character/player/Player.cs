using game.character.movement;
using game.character.movement.path;
using game.character.player;
using game.Item;
using System;
using UnityEngine;

namespace game.character.characters.player
{
    public class Player : ICharacter
    {
        public CharacterType PlayerType { get; private set; }

        public PlayerStats Stats { get; private set; }

        private PlayerEvents _playerEvents;

        public KeyboardPathFinder MovementPathCalc { get; set; }

        public ItemPickup ItemPickup;

        public void Construct(CharacterType playerType, PlayerStats stats, PlayerEvents playerEvents, MovementPath movementPath)
        {
            base.Construct();
            PlayerType = playerType;
            Stats = stats;
            _playerEvents = playerEvents;
            MovementPath = movementPath;

            MovementPath.OnTargetEnd += HandleTargetEnd;
            MovementPath.OnTargetStart += HandleTargetStart;
        }

        private void HandleTargetStart(object sender, EventArgs e)
        {
            _playerEvents.EmitTargetStart();
        }

        private void HandleTargetEnd(object sender, EventArgs e)
        {
            _playerEvents.EmitTargetEnd();
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