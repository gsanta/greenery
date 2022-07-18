using System;
using UnityEngine;

namespace AI.GridSystem
{
    public class Grid<TGridObject> where TGridObject : class
    {
        public readonly int Width;
        public readonly int Height;
        public readonly float CellSize;
        private TGridObject[] _gridArray;
        private readonly Vector2 _halfCell;
        private readonly Vector2 _offset;
        private readonly Vector2 _worldOffset;

        private readonly int _minX;
        private readonly int _maxX;
        private readonly int _minY;
        private readonly int _maxY;

        public Grid(int width, int height, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, float cellSize = 1.0f)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _halfCell = new Vector2(CellSize / 2.0f, CellSize / 2.0f);
            
            if (width % 2 == 1)
                _offset.x = 0.5f * CellSize;

            if (height % 2 == 1)
                _offset.y = 0.5f * CellSize;
            
            _minX = -(Width / 2);
            _maxX = Width % 2 == 1 ? Width / 2 : Width / 2 - 1;
            _minY = -(Height / 2);
            _maxY = Height % 2 == 1 ? Height / 2 : Height / 2 - 1;

            InitGridArray(createGridObject);
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

        private bool IsWithinGrid(int gridX, int gridY)
        {
            return gridX >= _minX && gridX <= _maxX && gridY >= _minY && gridY <= _maxY;
        }

        private int ToArrayIndex(int gridX, int gridY)
        {
            if (!IsWithinGrid(gridX, gridY))
            {
                return -1;
            }
            
            var x = gridX + Width / 2;
            var y = Height / 2 - gridY;

            if (Height % 2 == 0)
            {
                y -= 1;
            }

            return y * Width + x;
        }

        private void InitGridArray(Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
        {
            var xOffset = Width / 2;
            var yOffset = Height % 2 == 0 ? Height / 2 - 1 : Height / 2;
            
            _gridArray = new TGridObject[Width * Height];
            for (var i = 0; i < Width * Height; i++)
            {
                var x = i % Width;
                var y = i / Width;
                
                _gridArray[i] = createGridObject(this, x - xOffset, yOffset - y);
            }
        }
    }
}
