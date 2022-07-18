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
        
        [Test]
        public void LeftNeighbour_OddWidthGrid_XMin1Node_ReturnsXMin2Node()
        {
            var grid = new Grid<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var leftNeighbour = grid.LeftNeighbour(-1, 0);
            
            Assert.AreEqual(-2, leftNeighbour.x);
            Assert.AreEqual(0, leftNeighbour.y);
        }
        
        [Test]
        public void LeftNeighbour_OddWidthGrid_XNub2Node_ReturnsNull()
        {
            var grid = new Grid<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var leftNeighbour = grid.LeftNeighbour(-2, 0);
            
            Assert.IsNull(leftNeighbour);
        }
        
        [Test]
        public void LeftNeighbour_EvenWidthGrid_NotLeftMostNode_ReturnsLeftNode()
        {
            var grid = new Grid<GridNode>(4, 4, (g, gx, gy) => new GridNode(gx, gy));

            var leftNeighbour = grid.LeftNeighbour(-1, 0);
            
            Assert.AreEqual(-2, leftNeighbour.x);
            Assert.AreEqual(0, leftNeighbour.y);
        }
        
        [Test]
        public void LeftNeighbour_EvenWidthGrid_LeftMostNode_ReturnsNull()
        {
            var grid = new Grid<GridNode>(4, 4, (g, gx, gy) => new GridNode(gx, gy));

            var leftNeighbour = grid.LeftNeighbour(-2, 0);
            
            Assert.IsNull(leftNeighbour);
        }
        
        [Test]
        public void RightNeighbour_OddWidthGrid_NotRightMostNode_ReturnsRightNode()
        {
            var grid = new Grid<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var rightNeighbour = grid.RightNeighbour(1, 0);
            
            Assert.AreEqual(2, rightNeighbour.x);
            Assert.AreEqual(0, rightNeighbour.y);
        }
        
        [Test]
        public void RightNeighbour_OddWidthGrid_RightMostNode_ReturnsNull()
        {
            var grid = new Grid<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var rightNeighbour = grid.RightNeighbour(2, 0);
            
            Assert.IsNull(rightNeighbour);
        }
        
        [Test]
        public void RightNeighbour_EvenWidthGrid_NotRightMostNode_ReturnsRightNode()
        {
            var grid = new Grid<GridNode>(4, 4, (g, gx, gy) => new GridNode(gx, gy));

            var rightNeighbour = grid.RightNeighbour(0, 0);
            
            Assert.AreEqual(1, rightNeighbour.x);
            Assert.AreEqual(0, rightNeighbour.y);
        }
        
        [Test]
        public void RightNeighbour_EvenWidthGrid_RightMostNode_ReturnsNull()
        {
            var grid = new Grid<GridNode>(4, 4, (g, gx, gy) => new GridNode(gx, gy));

            var rightNeighbour = grid.RightNeighbour(1, 0);
            
            Assert.IsNull(rightNeighbour);
        }
        
        [Test]
        public void TopNeighbour_OddHeightGrid_NotTopMostNode_ReturnsTopNode()
        {
            var grid = new Grid<GridNode>(4, 5, (g, gx, gy) => new GridNode(gx, gy));

            var topNeighbour = grid.TopNeighbour(1, 1);
            
            Assert.AreEqual(2, topNeighbour.y);
            Assert.AreEqual(1, topNeighbour.x);
        }
        
        [Test]
        public void TopNeighbour_OddHeightGrid_TopMostNode_ReturnsNull()
        {
            var grid = new Grid<GridNode>(4, 5, (g, gx, gy) => new GridNode(gx, gy));

            var topNeighbour = grid.TopNeighbour(1, 2);
            
            Assert.IsNull(topNeighbour);
        }
        
        [Test]
        public void TopNeighbour_EvenHeightGrid_NotTopMostNode_ReturnsTopNode()
        {
            var grid = new Grid<GridNode>(4, 4, (g, gx, gy) => new GridNode(gx, gy));

            var topNeighbour = grid.TopNeighbour(1, 0);
            
            Assert.AreEqual(1, topNeighbour.y);
            Assert.AreEqual(1, topNeighbour.x);
        }
        
        [Test]
        public void TopNeighbour_EvenHeightGrid_TopMostNode_ReturnsNull()
        {
            var grid = new Grid<GridNode>(4, 4, (g, gx, gy) => new GridNode(gx, gy));

            var topNeighbour = grid.TopNeighbour(1, 1);
            
            Assert.IsNull(topNeighbour);
        }
        
        [Test]
        public void BottomNeighbour_OddHeightGrid_NotBottomMostNode_ReturnsBottomNode()
        {
            var grid = new Grid<GridNode>(5, 5, (g, gx, gy) => new GridNode(gx, gy));
        
            var bottomNeighbour = grid.BottomNeighbour(2, -1);
            
            Assert.AreEqual(-2, bottomNeighbour.y);
            Assert.AreEqual(2, bottomNeighbour.x);        
        }
        
        [Test]
        public void BottomNeighbour_OddHeightGrid_BottomMostNode_ReturnsNull()
        {
            var grid = new Grid<GridNode>(5, 5, (g, gx, gy) => new GridNode(gx, gy));
        
            var bottomNeighbour = grid.BottomNeighbour(2, -2);
            
            Assert.IsNull(bottomNeighbour);
        }
        
        [Test]
        public void BottomNeighbour_EvenHeightGrid_NotBottomMostNode_ReturnsBottomNode()
        {
            var grid = new Grid<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));
        
            var bottomNeighbour = grid.BottomNeighbour(2, -1);
            
            Assert.AreEqual(-2, bottomNeighbour.y);
            Assert.AreEqual(2, bottomNeighbour.x);        
        }
        
        [Test]
        public void BottomNeighbour_EvenHeightGrid_BottomMostNode_ReturnsNull()
        {
            var grid = new Grid<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));
        
            var bottomNeighbour = grid.BottomNeighbour(2, -2);
            
            Assert.IsNull(bottomNeighbour);
        }
    }
}