using game.character.movement;
using game.character.utils;
using UnityEngine;

namespace game.scene.grid.path
{
    public class PathMovement : Movement
    {
        [SerializeField] private float speed = 6f;

        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Rigidbody2D _rigidBody;

        public void Construct()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_isPaused)
            {
                return;
            }

            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_isMoving)
            {
                _rigidBody.AddForce(_direction * speed);
                _moveDirection = MovementUtil.UpdateMoveDirection(_direction, _moveDirection);
            }
        }
    }
}