using Base.Input;
using game.character.characters.player;
using game.Common;
using game.scene.level;
using UnityEngine;

namespace game.character.movement.path
{
    public class KeyboardPathFinder : InputListener, Activateable
    {
        private Player _player;

        private Level _level;

        private Movement _movementPath;

        public KeyboardPathFinder(Player player, Level level, Movement movementPath)
        {
            _player = player;
            _level = level;
            _movementPath = movementPath;
        }

        public void Activate()
        {
            IsListenerDisabled = false;
        }

        public void Deactivate()
        {
            IsListenerDisabled = true;
        }

        public override void OnKeyPressed(InputInfo inputInfo)
        {
            if (!_movementPath.IsTargetReached)
            {
                return;
            }

            var playerGridPos = _level.Grid.GetGridPosition(_player.GetPosition());
            Vector2Int targetGridPos = playerGridPos.Value;

            Vector2 direction = default;

            var amount = inputInfo.IsShiftPressed ? 2 : 1;

            if (inputInfo.IsAPressed)
            {
                direction = Vector2.left;
                targetGridPos.x -= amount;
            }

            if (inputInfo.IsWPressed)
            {
                direction = Vector2.up;
                targetGridPos.y += amount;
            }

            if (inputInfo.IsDPressed)
            {
                direction = Vector2.right;
                targetGridPos.x += amount;
            }

            if (inputInfo.IsSPressed)
            {
                direction = Vector2.down;
                targetGridPos.y -= amount;
            }

            var node = _level.Grid.GetNode(targetGridPos.x, targetGridPos.y);

            if (node.IsWalkable)
            {
                var targetWorldPos = _level.Grid.GetWorldPosition(targetGridPos.x, targetGridPos.y);

                _movementPath.SetDestination(targetWorldPos);
                _movementPath.SetDirection(direction);
                _movementPath.IsTargetReached = false;
            }

        }

    }
}
