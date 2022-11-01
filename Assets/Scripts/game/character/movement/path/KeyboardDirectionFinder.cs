using UnityEngine;

namespace game.character.movement.path
{
    internal class KeyboardDirectionFinder : MonoBehaviour
    {

        private Movement _movementPath;

        public void Construct(Movement movementPath)
        {
            _movementPath = movementPath;
        }

        private void Update()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            _movementPath.IsTargetReached = horizontalMovement == 0 || verticalMovement == 0;
            _movementPath.SetDirection(new Vector2(horizontalMovement, verticalMovement));
        }
    }
}
