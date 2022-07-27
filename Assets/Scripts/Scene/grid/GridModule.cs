using Scene.grid.path;

namespace Scene.grid
{
    public class GridModule
    {
        public Grid<PathNode> Grid { get; private set; }
        
        public GridVisualizer<PathNode> GridVisualizer { get; }
        
        public PathFinding PathFinding { get; }

        public GridModule(GridSetup gridSetup)
        {
            InitGrid(gridSetup);
            GridVisualizer = new GridVisualizer<PathNode>(Grid);
            PathFinding = new PathFinding(Grid); 
            
            if (gridSetup.isShowDebug)
            {
                GridVisualizer.Show();
            }
        }

        private void InitGrid(GridSetup gridSetup)
        {
            var topLeftPos = gridSetup.topLeft.position;
            var bottomRightPos = gridSetup.bottomRight.position;
            var cellSize = gridSetup.cellSize;
            Grid = Grid<PathNode>.CreateFromWorldSize(topLeftPos, bottomRightPos, (g, x, y) => new PathNode(x, y), cellSize);
        }
    }
}