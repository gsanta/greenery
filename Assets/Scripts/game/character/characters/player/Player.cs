using game.character.ability;
using game.character.ability.health;
using game.character.ability.shoot;
using game.character.state;
using game.character.utils;
using game.tool;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class Player : MonoBehaviour, ICharacter
    {
        public float moveSpeed = 5f;
        
        public StateHandler States { get; private set; }
        public AbilityHandler Abilities { get; }

        private PlayerCommandHandler _commandHandler; 

        private Direction _moveDirection = Direction.Down;

        private Vector3 _movement;
        
        private Rigidbody2D _rigidBody;
        
        private Animator _animator;

        private Health _health;

        public ITool Weapon;

        private bool _isActive;

        public bool IsActive { get => _isActive; set => _isActive = value; }

        public void Construct()
        {
            States = new StateHandler();
            _commandHandler = new PlayerCommandHandler(this);
        }

        void Start()
        {
            _health = GetComponent<Health>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
        }
        
        void Update()
        {
            if (!_isActive)
            {
                return;
            }
            
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            _movement = new Vector2(horizontalMovement, verticalMovement);
            _commandHandler.Update();

            _moveDirection = MovementUtil.UpdateMoveDirection(_movement, _moveDirection);
            UpdateBlendTrees();

            _rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;
        }
        
        private void UpdateBlendTrees()
        {
            if (_movement.x == 0 && _movement.y == 0)
            {
                _animator.SetBool("isMoving", false);
            }
            else
            {
                _animator.SetFloat("horizontalMovement", _movement.x);
                _animator.SetFloat("verticalMovement", _movement.y);
                _animator.SetBool("isMoving", true);
            }
        }

        public Direction GetMoveDirection()
        {
            return _moveDirection;
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void SetMovement(Vector2 movement)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetMovement()
        {
            throw new System.NotImplementedException();
        }

        public Health GetHealth()
        {
            return _health;
        }
        
        public void Die()
        {
            Debug.Log("player is dead");
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}