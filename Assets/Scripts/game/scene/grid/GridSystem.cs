using game.scene.grid.path;
using game.scene.level;

namespace game.scene.grid
{
    public class GridSystem
    {
        public Grid<PathNode> Grid { get; set; }

        public PathFinding PathFinding { get; private set; }
        
        private GridFactory _gridFactory { get; set; }

        public GridSystem(Level level)
        {
            _gridFactory = new GridFactory(level);
            Grid = _gridFactory.CreateGrid();
            PathFinding = new PathFinding(Grid);
        }
    }
}