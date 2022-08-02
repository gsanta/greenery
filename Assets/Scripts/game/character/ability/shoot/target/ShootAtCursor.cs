using UnityEngine;

namespace game.character.ability.shoot.target
{
    class ShootAtCursor : IShootTarget
    {
        private ICharacter _character;

        public ShootAtCursor(ICharacter character)
        {
            _character = character;
        }

        public Vector2 GetTarget()
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (((Vector2)worldMousePos - _character.GetPosition()));
            direction.Normalize();

            return direction;
        }
    }
}
