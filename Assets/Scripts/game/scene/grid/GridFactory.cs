using game.scene.level;
using UnityEngine;

namespace game.scene.grid
{
    public class GridFactory
    {
        private Level _level;

        public GridFactory(Level level)
        {
            _level = level;
        }

        public Grid<PathNode> CreateGrid()
        {
            var topLeft = _level.Environment.TopLeft;
            var bottomRight = _level.Environment.BottomRight;
            var cellSize = _level.Environment.CellSize;
            
            return Grid<PathNode>.CreateFromWorldSize(topLeft, bottomRight, (g, x, y) =>
            {
                var isWalkable = _level.Environment.IsWalkable(new Vector2Int(x, y));
                
                return new PathNode(x, y)
                {
                    IsWalkable = isWalkable
                };
            }, cellSize);
        }
    }
}