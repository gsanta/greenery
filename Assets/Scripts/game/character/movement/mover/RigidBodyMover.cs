using game.character.movement.path;
using UnityEngine;

namespace game.character.movement
{
    public class RigidBodyMover : MonoBehaviour
    {
        [SerializeField] private float speed = 6f;

        [SerializeField] private bool _flipWhenMovingRight = true;

        protected bool _isPaused;

        protected Direction _moveDirection = Direction.Down;

        protected ICharacter _character;

        private MovementPath _movementPath;

        protected Vector2 _direction;

        protected bool _isMoving;

        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");

        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Rigidbody2D _rigidBody;

        private Vector3 _currentMovement;

        float t;

        Vector2 startPosition;

        Vector2 target;

        float timeToReachTarget;

        public void Construct(ICharacter character, MovementPath movementPath)
        {
            _character = character;
            _movementPath = movementPath;
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_isPaused)
            {
                return;
            }

            HandleMovement();
        }

        private void HandleMovement()
        {
            if (!_movementPath.IsTargetReached)
            {
                _rigidBody.AddForce(_movementPath.GetDirection() * speed);
            }

            if (_movementPath.GetDestination().HasValue)
            {
                var destination = _movementPath.GetDestination().Value;
                var position = transform.position;
                if (Vector2.Distance(position, destination) < 0.2f)
                {
                    _movementPath.IsTargetReached = true;
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
    }
}