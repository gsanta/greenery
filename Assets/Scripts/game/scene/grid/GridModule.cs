using game.scene.grid.path;
using game.scene.tile;

namespace game.scene.grid
{
    public class GridModule
    {
        private Grid<PathNode> Grid { get; set; }

        private GridVisualizer<PathNode> GridVisualizer { get; }
        
        public PathFinding PathFinding { get; }
        
        private GridFactory _gridFactory { get; }

        public GridModule(GridSetup gridSetup, TileModule tileModule)
        {
            _gridFactory = new GridFactory(tileModule, gridSetup);
            Grid = _gridFactory.CreateGrid();
            GridVisualizer = new GridVisualizer<PathNode>(Grid);
            PathFinding = new PathFinding(Grid);

            
            if (gridSetup.isShowDebug)
            {
                GridVisualizer.Show();
            }
        }
    }
}