using System;
using System.Collections.Generic;
using AI.state.character;
using AI.state.character.states;
using Character.ability;
using Character.ability.abilities.shoot;
using Character.player;
using Character.utils;
using Characters.Common;
using Characters.Enemies;
using GameLogic;
using UnityEngine;

namespace Character.enemy
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
        
        private Health _health;
        
        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Direction _moveDirection = Direction.Down;

        private bool _isActive = false;

        private RoamingState _roamingState;

        private List<IAbility> _abilities = new();
        
        public StateHandler StateHandler { get; private set; } 
        
        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, GameManager gameManager)
        {
            _playerStore = playerStore;
            _enemyStore = enemyStore;
            _gameManager = gameManager;

            StateHandler = new StateHandler();
            _abilities.Add(GetComponent<Shooting>());
        }

        private void Start()
        {
            _health = GetComponent<Health>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdateActive();
            
            if (!_isActive)
            {
                return;
            }
            
            var player = _playerStore.GetActivePlayer();
            var direction = player.transform.position - transform.position;
            var radians = Mathf.Atan2(direction.y, direction.x);
            var rotationVector = new Vector2((float) Math.Cos(radians), (float) Math.Sin(radians));
            direction.Normalize();
            _movement = direction;
            
            _moveDirection = MovementUtil.UpdateMoveDirection(_movement, _moveDirection);
        
            // _animator.SetFloat(HorizontalMovement, rotationVector.x);
            // _animator.SetFloat(VerticalMovement, rotationVector.y);

            StateHandler.ActiveState?.UpdateState();
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

        public Direction GetMoveDirection()
        {
            return _moveDirection;
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
            Destroy(gameObject);
        }
    }
}