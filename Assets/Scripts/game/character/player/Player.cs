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

        public ItemPickup ItemPickup;

        private bool _isCurrentPlayer;

        public bool IsCurrentPlayer {
            get => _isCurrentPlayer;
            set
            {
                _isCurrentPlayer = value;
                HandleSetCurrentPlayer();
            } 
        }

        private void HandleSetCurrentPlayer()
        {
            if (_isCurrentPlayer)
            {
                PathFinder.Activate();
            } else
            {
                PathFinder.Deactivate();
            }
        }

        public void Construct(CharacterType playerType, PlayerStats stats, PlayerEvents playerEvents, Movement movementPath)
        {
            base.Construct();
            PlayerType = playerType;
            Stats = stats;
            _playerEvents = playerEvents;
            Movement = movementPath;

            Movement.OnTargetEnd += HandleTargetEnd;
            Movement.OnTargetStart += HandleTargetStart;
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