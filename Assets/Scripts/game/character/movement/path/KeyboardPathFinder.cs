﻿using Base.Input;
using game.Common;
using game.scene.level;
using UnityEngine;

namespace game.character.movement.path
{
    public class KeyboardPathFinder : InputListener, Activateable
    {
        private ICharacter _character;

        private Level _level;

        public void SetLevel(Level level)
        {
            _level = level;
        }

        public void SetCharacter(ICharacter character)
        {
            _character = character;
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
            var movement = _character.Movement;
            if (!movement.IsTargetReached)
            {
                return;
            }

            var playerGridPos = _level.Grid.GetGridPosition(_character.GetPosition());
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

                movement.SetDestination(targetWorldPos);
                movement.SetDirection(direction);
                movement.IsTargetReached = false;
            }

        }

    }
}
