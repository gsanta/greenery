using game.scene.level;

namespace game.scene.grid
{
    public class GridFactory
    {
        public GridGraph<PathNode> CreateGrid(LevelBounds levelBounds, Environment environment)
        {
            var topLeft = levelBounds.TopLeft;
            var bottomRight = levelBounds.BottomRight;
            var cellSize = levelBounds.CellSize;
            
            var graph = GridGraph<PathNode>.CreateFromWorldSize(topLeft, bottomRight, (g, x, y) => new PathNode(x, y), cellSize);
            environment.SetUnWalkable(graph);

            return graph;
        }
    }
}