using System;
using System.Collections.Generic;
using game.character.ability.field_of_view;
using game.character.ability.health;
using game.character.ability.shoot;
using game.character.characters.player;
using game.character.movement;
using game.character.utils;
using game.scene.grid.path;
using game.scene.level;
using game.tool;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class Enemy : ICharacter
    {
        public float moveSpeed = 5f;
        
        private Animator _animator;
        
        private Vector2 _movement;
        
        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private GameManager _gameManager;

        public ShootingBehaviour ShootingBehaviour { get; private set; }
        
        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        // TODO: get rid of this reference
        public Level Level { set; get; }

        public FieldOfView FieldOfView { get; set; }

        private bool _isInitialized = false;

        private List<GameObject> destroyables = new();
        
        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, GameManager gameManager)
        {
            base.Construct(new WeaponHolder());
            _playerStore = playerStore;
            _enemyStore = enemyStore;
            _gameManager = gameManager;

            _isInitialized = true;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            ShootingBehaviour = GetComponent<ShootingBehaviour>();
            Movement = GetComponent<PathMovement>();
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            //if (!_isActive)
            //{
            //    return;
            //}
            
            var player = _playerStore.GetActivePlayer();
            var direction = player.transform.position - transform.position;
            var radians = Mathf.Atan2(direction.y, direction.x);
            var rotationVector = new Vector2((float) Math.Cos(radians), (float) Math.Sin(radians));
            direction.Normalize();
            _movement = direction;
            
            // _animator.SetFloat(HorizontalMovement, rotationVector.x);
            // _animator.SetFloat(VerticalMovement, rotationVector.y);

            States.ActiveState?.UpdateState();
        }

        // private void LateUpdate()
        // {
        //     MoveCharacter(_movement);
        // }

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