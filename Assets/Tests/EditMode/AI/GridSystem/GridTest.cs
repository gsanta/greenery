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

            var (x, y) = grid.GetGridPosition(new Vector2(9.9f, 7.9f));
            Assert.AreEqual(4, x);
            Assert.AreEqual(3, y);
        }
        
        [Test]
        public void GetGridPosition_OddWidthAndHeight_JustAboveZero_ReturnsX0Y0()
        {
            var grid = new Grid<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy), 2.0f);

            var (x, y) = grid.GetGridPosition(new Vector2(0.1f, 0.1f));
            Assert.AreEqual(x, 0);
            Assert.AreEqual(y, 0);
        }
        
        [Test]
        public void GetGridPosition_OddWidthAndHeight_JustBelowZero_ReturnsX0Y0()
        {
            var grid = new Grid<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy), 2.0f);

            var (x, y) = grid.GetGridPosition(new Vector2(-0.1f, -0.1f));
            Assert.AreEqual(x, 0);
            Assert.AreEqual(y, 0);
        }
        
        [Test]
        public void GetGridPosition_OddWidthAndHeight_JustAboveHalf_ReturnsX1Y1()
        {
            var grid = new Grid<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy));

            var (x, y) = grid.GetGridPosition(new Vector2(0.6f, 0.6f));
            Assert.AreEqual(x, 1);
            Assert.AreEqual(y, 1);
        }
        
        [Test]
        public void GetGridPosition_OddWidthAndHeight_JustBelowHalf_ReturnsX0Y0()
        {
            var grid = new Grid<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy));

            var (x, y) = grid.GetGridPosition(new Vector2(0.4f, 0.4f));
            Assert.AreEqual(x, 0);
            Assert.AreEqual(y, 0);
        }

        
        [Test]
        public void GetGridPosition_CellSize2_OddWidthAndHeight_JustAboveOne_ReturnsX1Y1()
        {
            var grid = new Grid<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy), 2f);

            var (x, y) = grid.GetGridPosition(new Vector2(1.1f, 1.1f));
            Assert.AreEqual(x, 1);
            Assert.AreEqual(y, 1);
        }
        
        [Test]
        public void GetGridPosition_CellSize2_OddWidthAndHeight_JustBelowOne_ReturnsX0Y0()
        {
            var grid = new Grid<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy), 2f);

            var (x, y) = grid.GetGridPosition(new Vector2(0.9f, 0.9f));
            Assert.AreEqual(x, 0);
            Assert.AreEqual(y, 0);
        }
    }
}