using game.character.utils;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;

        [SerializeField] private bool _flipWhenMovingRight;

        protected bool _isPaused;

        protected Direction _moveDirection = Direction.Down;

        protected ICharacter _character;

        protected Vector2 _direction;

        protected bool _isMoving;

        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");

        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Rigidbody2D _rigidBody;

        private Animator _animator;

        private Vector3 _currentMovement;

        public void Construct(ICharacter character)
        {
            _character = character;
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_isPaused)
            {
                return;
            }

            HandleMovement();
            UpdateBlendTrees();
            UpdateModelDirection();
        }

        private void HandleMovement()
        {
            if (_isMoving)
            {
                _rigidBody.AddForce(_direction * speed);
                _moveDirection = MovementUtil.UpdateMoveDirection(_direction, _moveDirection);

                var horizontalMovement = Input.GetAxisRaw("Horizontal");
                var verticalMovement = Input.GetAxisRaw("Vertical");
                _currentMovement = new Vector2(horizontalMovement, verticalMovement);
            }
        }

        private void UpdateBlendTrees()
        {
            if (!_animator)
            {
                return;
            }

            if (_currentMovement.x == 0 && _currentMovement.y == 0)
            {
                _animator.SetBool("isMoving", false);
            }
            else
            {
                _animator.SetFloat("horizontalMovement", _currentMovement.x);
                _animator.SetFloat("verticalMovement", _currentMovement.y);
                _animator.SetBool("isMoving", true);
            }
        }

        private void UpdateModelDirection()
        {
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
        }

        public void PauseUntil(float time)
        {
            _isPaused = true;
            Invoke(nameof(Unpause), time);
        }

        private void Unpause()
        {
            _isPaused = false;
        }

        public Direction GetLookDirection()
        {
            return _moveDirection;
        }

        Vector2 GetDirection()
        {
            return _direction;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void SetIsMoving(bool isMoving)
        {
            _isMoving = isMoving;
        }

        public bool IsMoving()
        {
            return _isMoving;
        }

        public List<Vector2> GetPath()
        {
            return new List<Vector2>();
        }
    }
}