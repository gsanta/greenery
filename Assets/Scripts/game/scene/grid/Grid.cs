using game.scene.grid.path;
using game.scene.level;

namespace game.scene.grid
{
    public class Grid
    {
        public GridGraph<PathNode> Graph { get; set; }

        public PathFinding PathFinding { get; private set; }
        
        private GridFactory _gridFactory { get; set; }

        private Level _level;
        
        public Grid(Level level)
        {
            _level = level;
        }

        public void Init()
        {
            _gridFactory = new GridFactory(_level.LevelBounds, _level.Environment);
            Graph = _gridFactory.CreateGrid();
            PathFinding = new PathFinding(Graph);
        }
    }
}