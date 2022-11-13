
using System.Collections.Generic;
using game.character.ability.field_of_view;
using game.character.ability.shoot;
using game.character.characters.player;
using game.character.player;
using game.Common;
using game.scene.grid;
using game.scene.level;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class Enemy : ICharacter
    {
        public float moveSpeed = 5f;
        
        private Animator _animator;
        
        private PlayerStore _playerStore;

        public ShootingBehaviour ShootingBehaviour { get; private set; }

        // TODO: get rid of this reference
        public Level Level { set; get; }

        public FieldOfView FieldOfView { get; set; }

        private List<GameObject> destroyables = new();
        
        public void Construct(PlayerStore playerStore, GridGraph grid, PlayerType playerType, CharacterEvents characterEvents, MovementHandler movementHandler)
        {
            base.Construct(playerType, grid, characterEvents, movementHandler);
            _playerStore = playerStore;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            ShootingBehaviour = GetComponent<ShootingBehaviour>();
        }

        public override void Die()
        {
            _playerStore.Remove(this);
            _animator.SetBool("isDead", true);

            destroyables.ForEach((destroyable) => Destroy(destroyable));

            Destroy(gameObject, 1);
        }
    }
}