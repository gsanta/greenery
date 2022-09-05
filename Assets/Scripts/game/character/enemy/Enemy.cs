using System;
using game.character.ability;
using game.character.ability.health;
using game.character.ability.shoot;
using game.character.characters.player;
using game.character.movement;
using game.character.state;
using game.character.utils;
using game.scene.grid.path;
using game.scene.level;
using game.tool;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class Enemy : MonoBehaviour, ICharacter
    {
        public float moveSpeed = 5f;
        
        private Animator _animator;
        
        private Rigidbody2D _rb;
        
        private Vector2 _movement;
        
        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private GameManager _gameManager;

        public ShootingBehaviour ShootingBehaviour { get; private set; }
        
        private Health _health;

        public IWeapon Weapon;

        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Direction _moveDirection = Direction.Down;

        private bool _isActive = false;

        public StateHandler States { get; private set; } 
        
        public AbilityHandler Abilities { get; private set; }
        
        public LevelName LevelName { set; get; }

        public IMovement Movement { get; private set; }

        private bool _isInitialized = false;
        
        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, GameManager gameManager)
        {
            _playerStore = playerStore;
            _enemyStore = enemyStore;
            _gameManager = gameManager;

            States = new StateHandler();
            Abilities = new AbilityHandler();

            _isInitialized = true;
        }

        private void Start()
        {
            _health = GetComponent<Health>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            ShootingBehaviour = GetComponent<ShootingBehaviour>();
            Movement = GetComponent<PathMovement>();
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

            UpdateActive();
            
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
            
            _moveDirection = MovementUtil.UpdateMoveDirection(_movement, _moveDirection);
        
            // _animator.SetFloat(HorizontalMovement, rotationVector.x);
            // _animator.SetFloat(VerticalMovement, rotationVector.y);

            States.ActiveState?.UpdateState();
        }

        private void UpdateActive()
        {
            if (!_gameManager.IsGameStarted())
            {
                if (_isActive)
                {
                    _isActive = false;
                }
            }
            else
            {
                _isActive = true;
            }
        }

        // private void LateUpdate()
        // {
        //     MoveCharacter(_movement);
        // }

        private void MoveCharacter(Vector2 direction)
        {
            _rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
        }

        public void SetMovement(Vector2 movement)
        {
            _movement = movement;
        }

        public Vector2 GetMovement()
        {
            return _movement;
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public Health GetHealth()
        {
            return _health;
        }

        public void Die()
        {
            _enemyStore.Remove(this);
            _animator.SetBool("isDead", true);
            Destroy(gameObject, 1);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}