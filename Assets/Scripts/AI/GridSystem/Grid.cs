using System;
using UnityEngine;

namespace AI.GridSystem
{
    public class Grid<TGridObject> where TGridObject : class
    {
        public readonly int Width;
        public readonly int Height;
        private readonly TGridObject[] _gridArray;
        public readonly float CellSize;
        private readonly Vector2 _halfCell;
        private readonly Vector2 _offset;
        private readonly Vector2 _worldOffset;

        public Grid(int width, int height, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, float cellSize = 1.0f)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _halfCell = new Vector2(CellSize / 2.0f, CellSize / 2.0f);

            _gridArray = new TGridObject[width * height];

            if (width % 2 == 1)
            {
                _offset.x = 0.5f * CellSize;
            }

            if (height % 2 == 1)
            {
                _offset.y = 0.5f * CellSize;
            }

            for (var i = 0; i < width * height; i++)
            {
                var x = i % width;
                var y = i / width;
                _gridArray[i] = createGridObject(this, x - width / 2, height / 2 - 1 - y);
            }
        }

        public bool IsWithinGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public void SetValue(int x, int y, TGridObject value)
        {
            _gridArray[ToArrayIndex(x, y)] = value;
        }

        public TGridObject[] GetAllNodes()
        {
            return _gridArray;
        }

        public TGridObject GetGridObject(int x, int y)
        {
            var index = ToArrayIndex(x, y);
            if (index < 0 || index > _gridArray.Length - 1)
            {
                return null;
            }
            return _gridArray[ToArrayIndex(x, y)];
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * CellSize + _halfCell;
        }

        public (int, int) GetGridPosition(Vector2 worldPosition)
        {
            var vec = (worldPosition + _offset) / CellSize;

            return ((int) vec.x, (int) vec.y);
        }

        public TGridObject TopNeighbour(int x, int y)
        {
            y += 1;

            return GetGridObject(x, y);
        }
        
        public TGridObject BottomNeighbour(int x, int y)
        {
            y -= 1;

            return GetGridObject(x, y);
        }
        
        public TGridObject LeftNeighbour(int x, int y)
        {
            x -= 1;

            return GetGridObject(x, y);
        }
        
        public TGridObject RightNeighbour(int x, int y)
        {
            x += 1;

            return GetGridObject(x, y);
        }

        private bool IsXWithinGrid(int x)
        {
            var xNormalized = x + Width / 2;
            return xNormalized >= 0 && xNormalized < Width;
            
            // Height / 2 - 1 - y
        }

        private int ToArrayIndex(int gridX, int gridY)
        {
            var x = gridX + Width / 2;
            var y = Height / 2 - gridY;

            if (Height % 2 == 0)
            {
                y -= 1;
            }

            return y * Width + x;
        }
    }
}
