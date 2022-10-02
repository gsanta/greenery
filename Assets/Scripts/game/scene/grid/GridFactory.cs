using game.scene.level;
using game.scene.level.obstacle;
using System.Collections.Generic;

namespace game.scene.grid
{
    public class GridFactory
    {
        private List<IObstacleCalculator> _obstacleCalcs;

        private EnvironmentData _environmentData;

        public GridFactory(EnvironmentData environmentData)
        {
            this._environmentData = environmentData;

            _obstacleCalcs = new()
            {
                new TileObstacleCalculator(environmentData),
                new ObjectObstacleCalculator(environmentData)
            };
        }

        public GridGraph CreateGrid()
        {
            var topLeft = _environmentData.TopLeft;
            var bottomRight = _environmentData.BottomRight;
            var cellSize = _environmentData.GridSize;
            
            var grid = GridGraph.CreateFromWorldSize(topLeft, bottomRight, (g, x, y) => new PathNode(x, y), cellSize);
            
            foreach (var obstacleCalc in _obstacleCalcs)
            {
                obstacleCalc.Calculate(grid);
            }

            return grid;
        }
    }
}