using game.scene.grid.path;
using game.scene.level;
using game.scene.tile;

namespace game.scene.grid
{
    public class GridSystem
    {
        public Grid<PathNode> Grid { get; set; }

        public GridVisualizer GridVisualizer { get; private set; }
        
        public PathFinding PathFinding { get; private set; }
        
        private GridFactory _gridFactory { get; set; }

        public GridSystem(GridVisualizer gridVisualizer)
        {

            GridVisualizer = gridVisualizer;
        }

        public void Setup(Level level)
        {
            _gridFactory = new GridFactory(level);
            Grid = _gridFactory.CreateGrid();
            PathFinding = new PathFinding(Grid);
        }
    }
}