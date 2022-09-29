using game.scene.grid;
using UnityEngine;

namespace game.scene.level.obstacle
{
    class TileObstacleCalculator : IObstacleCalculator
    {
        private EnvironmentData _environmentData;

        public TileObstacleCalculator(EnvironmentData environmentData)
        {
            _environmentData = environmentData;
        }

        public void Calculate(GridGraph<PathNode> grid)
        {
            var width = grid.Width;
            var height = grid.Height;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    grid.GetNode(i, j).IsWalkable = IsWalkable(new Vector2Int(i, j));

                }
            }
        }

        private bool IsWalkable(Vector2Int pos)
        {
            var gridCenter = _environmentData.GridCenter;
            var tile = _environmentData.TileMapObjects.GetTile(new Vector3Int(pos.x - gridCenter.x, pos.y - gridCenter.y, 0));

            return tile == null;
        }
    }
}
