using Base.Input;
using game.scene.level;
using System;
using UnityEngine;

namespace game.scene.grid
{
    public class OnHoverTileEventArgs : EventArgs
    {
        public PathNode node;

        public OnHoverTileEventArgs(PathNode node)
        {
            this.node = node;
        }
    }

    public class TileInputHandler : InputHandler
    {
        private Level _level;

        PathNode _hoveredNode;

        public event EventHandler<OnHoverTileEventArgs> OnHoverTile;

        public TileInputHandler(Level level) : base(InputHandlerType.TileHandler)
        {
            _level = level;
        }

        public PathNode GetHoveredNode()
        {
            return _hoveredNode;
        }

        public override void OnMouseMove(InputInfo inputInfo)
        {
            var grid = _level.Grid;

            var pos = grid.GetGridPosition(new Vector2(inputInfo.xPos, inputInfo.yPos));
            PathNode newNode = null;

            if (pos.HasValue)
            {
                newNode = grid.GetNode(pos.Value.x, pos.Value.y);
            }

            if (newNode != _hoveredNode)
            {
                _hoveredNode = newNode;
                EmitHoverTile(newNode);
            }
        }

        private void EmitHoverTile(PathNode node)
        {
            if (OnHoverTile != null)
            {
                var args = new OnHoverTileEventArgs(node);

                OnHoverTile(this, args);
            }
        }
    }
}
