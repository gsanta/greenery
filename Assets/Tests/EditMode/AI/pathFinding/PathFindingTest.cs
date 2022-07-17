
using AI.GridSystem;
using AI.pathFinding;
using NUnit.Framework;

namespace Tests.EditMode.AI.pathFinding
{
    public class PathFindingTest
    {
        [Test]
        public void FindPath()
        {
            var grid = new Grid<PathNode>(4, 5, (grid, x, y) => new PathNode(x, y), 2f);
            grid.GetGridObject(-1, -1).IsWalkable = false;
            grid.GetGridObject(-1, -2).IsWalkable = false;
            grid.GetGridObject(0, 1).IsWalkable = false;
            grid.GetGridObject(0, 2).IsWalkable = false;
            
            var startNode = grid.GetGridObject(-2, -2);
            var endNode = grid.GetGridObject(1, 2);
            
            var pathFinding = new PathFinding(grid);
            var path = pathFinding.FindPath(startNode, endNode);
        }
    }
}