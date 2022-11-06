
using System.Collections.Generic;
using game.character.ability.field_of_view;
using game.character.ability.shoot;
using game.character.characters.player;
using game.scene.level;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class Enemy : ICharacter
    {
        public float moveSpeed = 5f;
        
        private Animator _animator;
        
        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        public ShootingBehaviour ShootingBehaviour { get; private set; }

        // TODO: get rid of this reference
        public Level Level { set; get; }

        public FieldOfView FieldOfView { get; set; }

        private bool _isInitialized = false;

        private List<GameObject> destroyables = new();
        
        public void Construct(EnemyStore enemyStore, PlayerStore playerStore)
        {
            base.Construct();
            _playerStore = playerStore;
            _enemyStore = enemyStore;

            _isInitialized = true;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            ShootingBehaviour = GetComponent<ShootingBehaviour>();
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            var player = _playerStore.GetCurrentPlayer();
            var direction = player.transform.position - transform.position;
            direction.Normalize();

            //States.ActiveState?.UpdateState();
        }

        public override void Die()
        {
            _enemyStore.Remove(this);
            _animator.SetBool("isDead", true);

            destroyables.ForEach((destroyable) => Destroy(destroyable));

            Destroy(gameObject, 1);
        }

        public void  AddDestroyable(GameObject gameObject)
        {
            destroyables.Add(gameObject);
        }

        public List<GameObject> GetDestroyables()
        {
            return destroyables;
        }

        public void RemoveDestoyable(GameObject gameObject)
        {
            destroyables.Remove(gameObject);
        }
    }
}