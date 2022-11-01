using game.character.movement.path;
using UnityEngine;
using Time = UnityEngine.Time;

namespace game.character.movement
{
    public class LerpMover : MonoBehaviour
    {
        protected bool _isPaused;

        protected ICharacter _character;

        private Movement _movementPath;

        protected Vector2 _direction;

        protected bool _isMoving;

        float t;

        Vector2 startPosition;

        Vector2? target = null;

        float timeToReachTarget;

        public void Construct(ICharacter character, Movement movementPath)
        {
            _character = character;
            _movementPath = movementPath;
        }

        private void Update()
        {
            if (_movementPath.IsPaused)
            {
                return;
            }

            HandleDestination();
            HandleMovement();
        }

        private void HandleMovement()
        {

            if (!target.HasValue || _movementPath.IsTargetReached)
            {
                return;
            }

            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPosition, target.Value, t);

            if (t >= 1)
            {
                _movementPath.IsTargetReached = true;
            }
        }

        public void HandleDestination()
        {
            if (!_movementPath.GetDestination().HasValue || _movementPath.GetDestination().Value == target)
            {
                return;
            }

            t = 0;
            startPosition = transform.position;
            timeToReachTarget = 0.5f;
            target = _movementPath.GetDestination();
        }
    }
}