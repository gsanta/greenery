
using game.character.movement;
using game.character.utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.game.character.characters.player
{
    public class InputMovement : MonoBehaviour, IMovement
    {
        public float moveSpeed = 2.5f;

        [SerializeField] private bool _flipWhenMovingRight;
        
        private Animator _animator;

        private Vector3 _movement;

        private Direction _moveDirection = Direction.Down;
        
        private Rigidbody2D _rigidBody;

        private bool _isPaused;

        private bool _isFlipped;

        public void PauseUntil(float time)
        {
            _isPaused = true;
            Invoke(nameof(Unpause), time);
        }

        private void Unpause()
        {
            _isPaused = false;
        }

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (_isPaused)
            {
                return;
            }

            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");
            _movement = new Vector2(horizontalMovement, verticalMovement);
            _rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;
            _moveDirection = MovementUtil.UpdateMoveDirection(_movement, _moveDirection);

            if (_flipWhenMovingRight)
            {
                if (_moveDirection == Direction.Right)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }

            UpdateBlendTrees();
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

        public Direction GetDirection()
        {
            return _moveDirection;
        }

        public List<Vector2> GetPath()
        {
            return new List<Vector2>();
        }
    }
}