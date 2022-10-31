using Base.Input;
using game.scene.level;
using UnityEngine;

namespace game.character.movement.path
{
    public class KeyboardPathFinder : InputListener
    {
        private ICharacter _character;

        private Level _level;

        private MovementPath _movementPath;

        public KeyboardPathFinder(ICharacter character, Level level, MovementPath movementPath)
        {
            _character = character;
            _level = level;
            _movementPath = movementPath;
        }

        public override void OnKeyPressed(InputInfo inputInfo)
        {
            if (!_movementPath.IsTargetReached)
            {
                return;
            }

            var playerGridPos = _level.Grid.GetGridPosition(_character.GetPosition());
            Vector2Int targetGridPos = playerGridPos.Value;

            Vector2 direction = default;

            if (inputInfo.IsAPressed)
            {
                direction = Vector2.left;
                targetGridPos.x -= 1;
            }

            if (inputInfo.IsWPressed)
            {
                direction = Vector2.up;
                targetGridPos.y += 1;
            }

            if (inputInfo.IsDPressed)
            {
                direction = Vector2.right;
                targetGridPos.x += 1;
            }

            if (inputInfo.IsSPressed)
            {
                direction = Vector2.down;
                targetGridPos.y -= 1;
            }

            var targetWorldPos = _level.Grid.GetWorldPosition(targetGridPos.x, targetGridPos.y);

            _movementPath.SetDestination(targetWorldPos);
            _movementPath.SetDirection(direction);
            _movementPath.IsTargetReached = false;
        }

    }
}
