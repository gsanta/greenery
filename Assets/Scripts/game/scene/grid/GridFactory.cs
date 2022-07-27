using game.scene.tile;
using UnityEngine;

namespace game.scene.grid
{
    public class GridFactory
    {
        private TileModule _tileModule;

        private GridSetup _gridSetup;

        public GridFactory(TileModule tileModule, GridSetup gridSetup)
        {
            _tileModule = tileModule;
            _gridSetup = gridSetup;
        }

        public Grid<PathNode> CreateGrid()
        {
            var topLeftPos = _gridSetup.topLeft.position;
            var bottomRightPos = _gridSetup.bottomRight.position;
            var cellSize = _gridSetup.cellSize;
            return Grid<PathNode>.CreateFromWorldSize(topLeftPos, bottomRightPos, (g, x, y) =>
            {
                var isWalkable = _tileModule.TileMapBase.IsWalkable(new Vector2Int(x, y));
                return new PathNode(x, y);
            }, cellSize);
        }
    }
}