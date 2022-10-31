using UnityEngine;

namespace game.character.movement
{
    public class KeyboardMover : MonoBehaviour
    {

        private Movement _movement;

        public void Construct(Movement movement)
        {
            _movement = movement;
        }

        private void Update()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            _movement.SetIsMoving(horizontalMovement != 0 || verticalMovement != 0);
            _movement.SetDirection(new Vector2(horizontalMovement, verticalMovement));
        }
    }
}
