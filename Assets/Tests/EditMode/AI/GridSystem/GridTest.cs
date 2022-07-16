using AI.GridSystem;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.AI.GridSystem
{
    public class GridTest
    {

        [Test]
        public void GetWorldPosition_DefaultGridWidth10Height8_AtXMin5YMin4_ReturnsXMin5YMin4()
        {
            var grid = new Grid<GridNode>(10, 8, (Grid<GridNode> g, int gx, int gy) => new GridNode(gx, gy));

            var worldPosition = grid.GetWorldPosition(-5, -4);
            Assert.AreEqual(worldPosition.x, -4.5f, 0.01f);
            Assert.AreEqual(worldPosition.y, -3.5f, 0.01f);
        }
        
        [Test]
        public void GetWorldPosition_GridCellSize2Width10Height8_AtX1Y2_ReturnsXMin5YMin4()
        {
            var grid = new Grid<GridNode>(10, 8, (Grid<GridNode> g, int gx, int gy) => new GridNode(gx, gy), 2f);

            var worldPosition = grid.GetWorldPosition(1, 2);
            Assert.AreEqual(worldPosition.x, 3f, 0.01f);
            Assert.AreEqual(worldPosition.y, 5f, 0.01f);
        }
        
        [Test]
        public void GetGridPosition_DefaultGridWidth10Height8_AtXMin5YMin4_ReturnsXMin5YMin4()
        {
            var grid = new Grid<GridNode>(10, 8, (Grid<GridNode> g, int gx, int gy) => new GridNode(gx, gy));

            var (x, y) = grid.GetGridPosition(new Vector2(-5, -4));
            Assert.AreEqual(x, -5);
            Assert.AreEqual(y, -4);
        }
        
        [Test]
        public void GetGridPosition_DefaultGridWidth10Height8_AtXMin0Dot1Y0Dot1_ReturnsX0Y0()
        {
            var grid = new Grid<GridNode>(10, 8, (Grid<GridNode> g, int gx, int gy) => new GridNode(gx, gy));

            var (x, y) = grid.GetGridPosition(new Vector2(-0.1f, 0.1f));
            Assert.AreEqual(x, 0);
            Assert.AreEqual(y, 0);
        }
        
        [Test]
        public void GetGridPosition_GridCellSize2Width10Height8_AtX10Y8_ReturnsX4Y3()
        {
            var grid = new Grid<GridNode>(10, 8, (Grid<GridNode> g, int gx, int gy) => new GridNode(gx, gy), 2.0f);

            var (x, y) = grid.GetGridPosition(new Vector2(10, 8));
            Assert.AreEqual(x, 4);
            Assert.AreEqual(y, 3);
        }
    }
}