using game.scene.grid.path;
using game.scene.level;
using game.scene.tile;

namespace game.scene.grid
{
    public class GridSystem
    {
        public Grid<PathNode> Grid { get; set; }

        private GridVisualizer GridVisualizer { get; }
        
        public PathFinding PathFinding { get; }
        
        private GridFactory _gridFactory { get; }

        public GridSystem(Level level)
        {
            _gridFactory = new GridFactory(level);
            Grid = _gridFactory.CreateGrid();
            GridVisualizer = new GridVisualizer(Grid);
            PathFinding = new PathFinding(Grid);
            
            if (true)
            {
                GridVisualizer.Show();
            }
        }
    }
}