using game.scene.grid;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.AI.GridSystem
{
    public class GridTest
    {

        [Test]
        public void GetWorldPosition_Width10Height8_AtX5Y4_ReturnsWorldPos()
        {
            var grid = new GridGraph<GridNode>(10, 8, (GridGraph<GridNode> g, int gx, int gy) => new GridNode(gx, gy));

            var worldPosition = grid.GetWorldPosition(5, 4);
            Assert.AreEqual(worldPosition.x, 5f, 0.01f);
            Assert.AreEqual(worldPosition.y, 4f, 0.01f);
        }
        
        [Test]
        public void GetWorldPosition_Width10Height8_AtX0Y0_ReturnsWorldPos()
        {
            var grid = new GridGraph<GridNode>(10, 8, (GridGraph<GridNode> g, int gx, int gy) => new GridNode(gx, gy));

            var worldPosition = grid.GetWorldPosition(0, 0);
            Assert.AreEqual(worldPosition.x, 0f, 0.01f);
            Assert.AreEqual(worldPosition.y, 0f, 0.01f);
        }

        [Test]
        public void GetWorldPosition_CellSize2Width10Height8_AtX1Y2_ReturnsWorldPos()
        {
            var grid = new GridGraph<GridNode>(10, 8, (GridGraph<GridNode> g, int gx, int gy) => new GridNode(gx, gy), 2f);

            var worldPosition = grid.GetWorldPosition(1, 2);
            Assert.AreEqual(worldPosition.x, 2f, 0.01f);
            Assert.AreEqual(worldPosition.y, 4f, 0.01f);
        }
        
        [Test]
        public void GetWorldPosition_WithOffsetAndWidth10Height8_AtX1Y2_ReturnsWorldPos()
        {
            var grid = new GridGraph<GridNode>(10, 8, (GridGraph<GridNode> g, int gx, int gy) => new GridNode(gx, gy), 1, new Vector2(-2, 2));

            var worldPosition = grid.GetWorldPosition(1, 2);
            Assert.AreEqual(-1f, worldPosition.x , 0.01f);
            Assert.AreEqual(4f, worldPosition.y, 0.01f);
        }
        
        [Test]
        public void GetGridPosition_Width10Height8_AtX0Y0_ReturnsWorldPos()
        {
            var grid = new GridGraph<GridNode>(10, 8, (GridGraph<GridNode> g, int gx, int gy) => new GridNode(gx, gy));

            var pos = grid.GetGridPosition(new Vector2(0, 0));
            Assert.AreEqual(0, pos.Value.x);
            Assert.AreEqual(0, pos.Value.y);
        }

        [Test]
        public void GetGridPosition_Width10Height8_AtXMin0Dot1Y0Dot1_ReturnsGridPos()
        {
            var grid = new GridGraph<GridNode>(10, 8, (GridGraph<GridNode> g, int gx, int gy) => new GridNode(gx, gy));

            var pos = grid.GetGridPosition(new Vector2(4.5f, 5.5f));
            Assert.AreEqual(4, pos.Value.x);
            Assert.AreEqual(5, pos.Value.y);
        }
        
        [Test]
        public void GetGridPosition_CellSize2Width10Height8_ReturnsGridPos()
        {
            var grid = new GridGraph<GridNode>(10, 8, (GridGraph<GridNode> g, int gx, int gy) => new GridNode(gx, gy), 2.0f);

            var pos = grid.GetGridPosition(new Vector2(9.9f, 7.9f));
            Assert.AreEqual(4, pos.Value.x);
            Assert.AreEqual(3, pos.Value.y);
        }
        
        [Test]
        public void GetGridPosition_XOutOfBoundsMin_ReturnsNullish()
        {
            var grid = new GridGraph<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy));

            var pos = grid.GetGridPosition(new Vector2(-0.1f, 1f));
            Assert.AreEqual(-1, pos.Value.x);
            Assert.AreEqual(-1, pos.Value.y);
        }
        
        [Test]
        public void GetGridPosition_XOutOfBoundsMax_ReturnsNullish()
        {
            var grid = new GridGraph<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy));

            var pos = grid.GetGridPosition(new Vector2(5.1f, 1f));
            Assert.AreEqual(pos.Value.x, -1);
            Assert.AreEqual(pos.Value.y, -1);
        }
        
        [Test]
        public void GetGridPosition_YOutOfBoundsMin_ReturnsNullish()
        {
            var grid = new GridGraph<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy));

            var pos = grid.GetGridPosition(new Vector2(1f, -0.1f));
            Assert.AreEqual(pos.Value.x, -1);
            Assert.AreEqual(pos.Value.y, -1);
        }
        
        [Test]
        public void GetGridPosition_YOutOfBoundsMax_ReturnsNullish()
        {
            var grid = new GridGraph<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy));

            var pos = grid.GetGridPosition(new Vector2(1f, 7.1f));
            Assert.AreEqual(pos.Value.x, -1);
            Assert.AreEqual(pos.Value.y, -1);
        }
        
        [Test]
        public void GetGridPosition_WithOffset_ReturnsGridPos()
        {
            var grid = new GridGraph<GridNode>(5, 7, (g, gx, gy) => new GridNode(gx, gy), 1f, new Vector2(2, -2));

            var pos = grid.GetGridPosition(new Vector2(3f, -2f));
            Assert.AreEqual(pos.Value.x, 1);
            Assert.AreEqual(pos.Value.y, 0);
        }
        
        [Test]
        public void LeftNeighbour_HasLeftNeighbour_ReturnsLeftNeighbour()
        {
            var grid = new GridGraph<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var leftNeighbour = grid.LeftNeighbour(1, 0);
            
            Assert.AreEqual(0, leftNeighbour.x);
            Assert.AreEqual(0, leftNeighbour.y);
        }

        [Test]
        public void LeftNeighbour_NoLeftNeighbour_ReturnsNull()
        {
            var grid = new GridGraph<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var leftNeighbour = grid.LeftNeighbour(0, 0);

            Assert.IsNull(leftNeighbour);
        }

        [Test]
        public void RightNeighbour_HasRightNeighbour_ReturnsRightNeighbour()
        {
            var grid = new GridGraph<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var rightNeighbour = grid.RightNeighbour(3, 0);
            
            Assert.AreEqual(4, rightNeighbour.x);
            Assert.AreEqual(0, rightNeighbour.y);
        }
        
        [Test]
        public void RightNeighbour_NoRightNeighbour_ReturnsNull()
        {
            var grid = new GridGraph<GridNode>(5, 4, (g, gx, gy) => new GridNode(gx, gy));

            var rightNeighbour = grid.RightNeighbour(4, 0);
            
            Assert.IsNull(rightNeighbour);
        }

        [Test]
        public void TopNeighbour_HasTopNode_ReturnsTopNode()
        {
            var grid = new GridGraph<GridNode>(4, 5, (g, gx, gy) => new GridNode(gx, gy));

            var topNeighbour = grid.TopNeighbour(1, 3);
            
            Assert.AreEqual(4, topNeighbour.y);
            Assert.AreEqual(1, topNeighbour.x);
        }
        
        [Test]
        public void TopNeighbour_NoTopNode_ReturnsNull()
        {
            var grid = new GridGraph<GridNode>(4, 5, (g, gx, gy) => new GridNode(gx, gy));

            var topNeighbour = grid.TopNeighbour(1, 4);
            
            Assert.IsNull(topNeighbour);
        }
        
        [Test]
        public void BottomNeighbour_HasBottomNeighbour_ReturnsBottomNeighbour()
        {
            var grid = new GridGraph<GridNode>(5, 5, (g, gx, gy) => new GridNode(gx, gy));
        
            var bottomNeighbour = grid.BottomNeighbour(2, 1);
            
            Assert.AreEqual(0, bottomNeighbour.y);
            Assert.AreEqual(2, bottomNeighbour.x);        
        }
        
        [Test]
        public void BottomNeighbour_NoBottomNeighbour_ReturnsNull()
        {
            var grid = new GridGraph<GridNode>(5, 5, (g, gx, gy) => new GridNode(gx, gy));
        
            var bottomNeighbour = grid.BottomNeighbour(2, 0);
            
            Assert.IsNull(bottomNeighbour);
        }
    }
}