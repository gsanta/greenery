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
            var topLeft = _level.TopLeft;
            var bottomRight = _level.BottomRight;
            var cellSize = _level.CellSize;
            
            return Grid<PathNode>.CreateFromWorldSize(topLeft, bottomRight, (g, x, y) =>
            {
                var isWalkable = _level.IsWalkable(new Vector2Int(x, y));
                
                return new PathNode(x, y)
                {
                    IsWalkable = isWalkable
                };
            }, cellSize);
        }
    }
}