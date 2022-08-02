
using UnityEngine;

namespace game.character.ability.shoot.target
{
    class MoveDirectionTarget : IShootTarget
    {
        private ICharacter _character;

        public MoveDirectionTarget(ICharacter character)
        {
            _character = character;
        }

        public Vector2 GetTarget()
        {
            var shootingDir = DirectionHelper.DirToVector(_character.GetMoveDirection());

            return shootingDir;
        }
    }
}
