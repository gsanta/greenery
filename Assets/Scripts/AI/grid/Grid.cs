using System;
using UnityEngine;

namespace AI.grid
{
    public class Grid<TNode> where TNode : class
    {
        public readonly int Width;
        public readonly int Height;
        public readonly float CellSize;
        private TNode[] _gridArray;
        private readonly Vector2 _halfCellSize;
        private readonly Vector2 _worldOffset;

        public static Grid<TNode> CreateFromWorldSize(Vector2 topLeft, Vector2 bottomRight , Func<Grid<TNode>, int, int, TNode> createGridObject, float cellSize = 1.0f)
        {
            var worldWidth = Mathf.Abs(topLeft.x - bottomRight.x);
            var worldMinX = topLeft.x;
            var worldHeight = Mathf.Abs(topLeft.y - bottomRight.y);
            var worldMinY = bottomRight.y;
        
            var gridWidth = (int) (worldWidth / cellSize) + 1;
            var gridHeight = (int) (worldHeight / cellSize) + 1;

            return new Grid<TNode>(gridWidth, gridHeight, createGridObject, cellSize,
                new Vector2(worldMinX, worldMinY));
        }

        public Grid(int width, int height, Func<Grid<TNode>, int, int, TNode> createGridObject, float cellSize = 1.0f, Vector2 worldOffset = default)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _worldOffset = worldOffset;
            _halfCellSize = new Vector2(CellSize / 2.0f, CellSize / 2.0f);

            InitGridArray(createGridObject);
        }

        public TNode[] GetAllNodes()
        {
            return _gridArray;
        }

        public TNode GetNode(int x, int y)
        {
            var index = ToArrayIndex(x, y);
            if (index < 0 || index > _gridArray.Length - 1)
            {
                return null;
            }
            return _gridArray[index];
        }
        
        public Vector3 GetWorldPosition(int x, int y, float worldZ)
        {
            var worldPos = GetWorldPosition(x, y);
            return new Vector3(worldPos.x, worldPos.y, worldZ);
        }

        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * CellSize + _worldOffset  + _halfCellSize;
        }

        public (int, int) GetGridPosition(Vector2 worldPosition)
        {
            var vec = (worldPosition - _worldOffset) / CellSize;
            var x = (int) Mathf.Floor(vec.x);
            var y = (int) Mathf.Floor(vec.y);
            
            return !IsWithinGrid(x, y) ? (-1, -1) : (x, y);
        }

        public TNode TopNeighbour(int x, int y)
        {
            y += 1;

            return GetNode(x, y);
        }
        
        public TNode BottomNeighbour(int x, int y)
        {
            y -= 1;

            return GetNode(x, y);
        }
        
        public TNode LeftNeighbour(int x, int y)
        {
            x -= 1;

            return GetNode(x, y);
        }
        
        public TNode RightNeighbour(int x, int y)
        {
            x += 1;

            return GetNode(x, y);
        }

        private bool IsWithinGrid(int gridX, int gridY)
        {
            return gridX >= 0 && gridX < Width && gridY >= 0 && gridY < Height;
        }

        private int ToArrayIndex(int gridX, int gridY)
        {
            if (!IsWithinGrid(gridX, gridY))
            {
                return -1;
            }

            return gridY * Width + gridX;
        }

        private void InitGridArray(Func<Grid<TNode>, int, int, TNode> createGridObject)
        {
            _gridArray = new TNode[Width * Height];
            for (var i = 0; i < Width * Height; i++)
            {
                var x = i % Width;
                var y = i / Width;
                
                _gridArray[i] = createGridObject(this, x, y);
            }
        }
    }
}
