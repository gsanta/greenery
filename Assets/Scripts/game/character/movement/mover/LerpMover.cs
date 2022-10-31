using game.character.movement.path;
using game.character.utils;
using UnityEngine;
using Time = UnityEngine.Time;

namespace game.character.movement
{
    public class LerpMover : MonoBehaviour
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
            if (_movementPath.IsPaused)
            {
                return;
            }

            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_movementPath.GetDestination().HasValue && _movementPath.GetDestination().Value != target)
            {
                UpdateDestination();
            }

            if (!_movementPath.IsTargetReached)
            {

                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector3.Lerp(startPosition, target, t);

                if (t >= 1)
                {
                    _movementPath.IsTargetReached = true;
                }

                _moveDirection = MovementUtil.UpdateMoveDirection(_movementPath.GetDirection(), _moveDirection);
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

        Vector2 GetDirection()
        {
            return _direction;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void UpdateDestination()
        {
            t = 0;
            startPosition = transform.position;
            timeToReachTarget = 0.5f;
            target = _movementPath.GetDestination().Value;
        }

        public void SetIsMoving(bool isMoving)
        {
            _isMoving = isMoving;
        }

        public bool IsMoving()
        {
            return _isMoving;
        }
    }
}