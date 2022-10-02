using System;
using UnityEngine;

namespace game.scene.grid
{
    public class GridGraph
    {
        public readonly int Width;
        
        public readonly int Height;
        
        public readonly float CellSize;
        
        private PathNode[] _gridArray;
        
        private readonly Vector2 _worldSize;

        public static GridGraph CreateFromWorldSize(Vector2 topLeft, Vector2 bottomRight , Func<GridGraph, int, int, PathNode> createGridObject, float cellSize = 1.0f)
        {
            var worldWidth = Mathf.Abs(topLeft.x - bottomRight.x);
            var worldMinX = topLeft.x;
            var worldHeight = Mathf.Abs(topLeft.y - bottomRight.y);
            var worldMinY = bottomRight.y;
        
            var gridWidth = (int) (worldWidth / cellSize) + 1;
            var gridHeight = (int) (worldHeight / cellSize) + 1;

            

            return new GridGraph(gridWidth, gridHeight, createGridObject, cellSize,
                new Vector2(worldWidth, worldHeight));
        }

        public GridGraph(int width, int height, Func<GridGraph, int, int, PathNode> createGridObject, float cellSize = 1.0f, Vector2 worldSize = default)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _worldSize = worldSize;

            InitGridArray(createGridObject);
        }

        public PathNode[] GetAllNodes()
        {
            return _gridArray;
        }


        public PathNode GetNode(int x, int y)
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
            return new Vector2(x, y) * CellSize - _worldSize / 2 + new Vector2(CellSize, CellSize) / 2;
        }

        public Vector2 GetRandomWorldPosition()
        {
            var x = UnityEngine.Random.Range(0, Width);
            var y = UnityEngine.Random.Range(0, Height);

            return GetWorldPosition(x, y);
        }

        public Vector2Int? GetGridPosition(Vector2 worldPosition)
        {
            var vec = (worldPosition + _worldSize / 2) / CellSize;
            var x = (int) Mathf.Floor(vec.x);
            var y = (int) Mathf.Floor(vec.y);
            
            return !IsWithinGrid(x, y) ? null : new Vector2Int(x, y);
        }

        public Vector2Int GetRandomGridPosition()
        {
            var posX = UnityEngine.Random.Range(0, Width - 1);
            var posY = UnityEngine.Random.Range(0, Height - 1);

            return new Vector2Int(posX, posY);
        }

        public PathNode GetRandomNode(bool onlyWalkable = true)
        {
            PathNode randomNode;
            do
            {
                var randomPos = GetRandomGridPosition();
                randomNode = GetNode(randomPos.x, randomPos.y);

            } while (onlyWalkable && (randomNode == null || !randomNode.IsWalkable));

            return randomNode;
        }

        public PathNode TopNeighbour(int x, int y)
        {
            y += 1;

            return GetNode(x, y);
        }
        
        public PathNode BottomNeighbour(int x, int y)
        {
            y -= 1;

            return GetNode(x, y);
        }
        
        public PathNode LeftNeighbour(int x, int y)
        {
            x -= 1;

            return GetNode(x, y);
        }
        
        public PathNode RightNeighbour(int x, int y)
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

        private void InitGridArray(Func<GridGraph, int, int, PathNode> createGridObject)
        {
            _gridArray = new PathNode[Width * Height];
            for (var i = 0; i < Width * Height; i++)
            {
                var x = i % Width;
                var y = i / Width;
                
                _gridArray[i] = createGridObject(this, x, y);
            }
        }
    }
}
