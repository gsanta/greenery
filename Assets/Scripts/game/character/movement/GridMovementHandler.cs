
using game.character.player;
using game.scene.level;
using System;
using UnityEngine;

namespace game.character.movement
{
    public class GridMovementHandler
    {

        private CharacterEvents _characterEvents;

        private LevelStore _levelStore;

        private MovementManager _movementManager;

        public GridMovementHandler(CharacterEvents characterEvents, LevelStore levelStore, MovementManager movementManager)
        {
            _characterEvents = characterEvents;

            _levelStore = levelStore;

            _movementManager = movementManager;

            _characterEvents.OnGridChange += HandleGridChange;
        }

        private void HandleGridChange(object sender, OnGridChangeEventArgs e)
        {
            if (e.from != null) {
                e.from.character = null;
            }
            if (e.to.character != null)
            {
                var dir = GetCollisionDir(e.from, e.to);
                CreateAction(e.to.character, e.to, dir);
            }
            e.to.character = e.character;
        }

        private Direction GetCollisionDir(PathNode from, PathNode to)
        {
            if (from.X < to.X)
            {
                return Direction.Right;
            } else if (from.X > to.X)
            {
                return Direction.Left;
            } else if (from.Y < to.Y)
            {
                return Direction.Up;
            } else
            {
                return Direction.Down;
            }
        }

        private void CreateAction(ICharacter character, PathNode node, Direction dir)
        {
            var gridPos = new Vector2Int(node.X, node.Y);
            gridPos += DirectionHelper.DirToVectorInt(dir);

            //var newNode = _levelStore.ActiveLevel.Grid.GetNode(gridPos.x, gridPos.y);
            var targetWorldPos = _levelStore.ActiveLevel.Grid.GetWorldPosition(gridPos.x, gridPos.y);


            _movementManager.AddMovementAction(new MovementAction(character, targetWorldPos));
        }
    }
}
