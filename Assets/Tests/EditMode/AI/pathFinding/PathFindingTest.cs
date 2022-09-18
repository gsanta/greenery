
using game.scene.grid;
using game.scene.grid.path;
using NUnit.Framework;

namespace Tests.EditMode.AI.pathFinding
{
    public class PathFindingTest
    {
        [Test]
        public void FindPath_ValidPathExists_ReturnsPath()
        {
            var grid = new GridGraph<PathNode>(4, 5, (grid, x, y) => new PathNode(x, y), 2f);
            grid.GetNode(1, 0).IsWalkable = false;
            grid.GetNode(1, 1).IsWalkable = false;
            grid.GetNode(2, 3).IsWalkable = false;
            grid.GetNode(2, 4).IsWalkable = false;
            
            var startNode = grid.GetNode(0, 0);
            var endNode = grid.GetNode(3, 4);
            
            var pathFinding = new PathFinding();
            var path = pathFinding.FindPath(grid, startNode, endNode);

            var expectedPath = new int[,]
            {
                {0, 0},
                {0, 1},
                {0, 2},
                {1, 2},
                {2, 2},
                {3, 2},
                {3, 3},
                {3, 4}
            };
            
            Assert.AreEqual(path[0].X, expectedPath[0, 0]);
            Assert.AreEqual(path[0].Y, expectedPath[0, 1]);
            Assert.AreEqual(path[1].X, expectedPath[1, 0]);
            Assert.AreEqual(path[1].Y, expectedPath[1, 1]);
            Assert.AreEqual(path[2].X, expectedPath[2, 0]);
            Assert.AreEqual(path[2].Y, expectedPath[2, 1]);
            Assert.AreEqual(path[3].X, expectedPath[3, 0]);
            Assert.AreEqual(path[3].Y, expectedPath[3, 1]);
            Assert.AreEqual(path[4].X, expectedPath[4, 0]);
            Assert.AreEqual(path[4].Y, expectedPath[4, 1]);
            Assert.AreEqual(path[5].Y, expectedPath[5, 1]);
            Assert.AreEqual(path[5].Y, expectedPath[5, 1]);
            Assert.AreEqual(path[6].Y, expectedPath[6, 1]);
            Assert.AreEqual(path[6].Y, expectedPath[6, 1]);
        }

        [Test]
        public void FindPath_NoValidPath_ReturnsNull()
        {
            var grid = new GridGraph<PathNode>(4, 5, (grid, x, y) => new PathNode(x, y), 2f);
            grid.GetNode(1, 0).IsWalkable = false;
            grid.GetNode(1, 1).IsWalkable = false;
            grid.GetNode(1, 2).IsWalkable = false;
            grid.GetNode(2, 3).IsWalkable = false;
            grid.GetNode(2, 4).IsWalkable = false;
            
            var startNode = grid.GetNode(0, 0);
            var endNode = grid.GetNode(3, 4);
            
            var pathFinding = new PathFinding();
            var path = pathFinding.FindPath(grid, startNode, endNode);
            
            Assert.IsNull(path);
        }
    }
}