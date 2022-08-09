using game.scene.level;
using UnityEngine;

namespace game.scene.grid
{
    public class GridFactory
    {
        private Environment _environment;

        private LevelBounds _levelBounds;

        public GridFactory(LevelBounds levelBounds, Environment environment)
        {
            _levelBounds = levelBounds;
            _environment = environment;
        }

        public GridGraph<PathNode> CreateGrid()
        {
            var topLeft = _levelBounds.TopLeft;
            var bottomRight = _levelBounds.BottomRight;
            var cellSize = _levelBounds.CellSize;
            
            var graph = GridGraph<PathNode>.CreateFromWorldSize(topLeft, bottomRight, (g, x, y) => new PathNode(x, y), cellSize);
            _environment.SetUnWalkable(graph);

            return graph;
        }
    }
}