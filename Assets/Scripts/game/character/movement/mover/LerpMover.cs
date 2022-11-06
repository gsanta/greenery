using game.character.movement.path;
using UnityEngine;
using Time = UnityEngine.Time;

namespace game.character.movement
{
    public class LerpMover : MonoBehaviour
    {
        protected ICharacter _character;

        private Movement _movement;

        float t;

        Vector2 startPosition;

        Vector2? target = null;

        float timeToReachTarget;

        public void Construct(ICharacter character, Movement movement)
        {
            _character = character;
            _movement = movement;
        }

        private void Update()
        {
            if (_movement.IsPaused)
            {
                return;
            }

            HandleDestination();
            HandleMovement();
        }

        private void HandleMovement()
        {

            if (!target.HasValue || _movement.IsTargetReached)
            {
                return;
            }

            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPosition, target.Value, t);

            if (t >= 1)
            {
                _movement.IsTargetReached = true;
            }
        }

        public void HandleDestination()
        {
            if (!_movement.GetDestination().HasValue || _movement.GetDestination().Value == target)
            {
                return;
            }

            t = 0;
            startPosition = transform.position;
            timeToReachTarget = 0.5f;
            target = _movement.GetDestination();
        }
    }
}