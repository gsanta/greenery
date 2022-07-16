using System;
using UnityEngine;

namespace AI.GridSystem
{
    public class Grid<TGridObject>
    {
        public readonly int Width;
        public readonly int Height;
        private readonly TGridObject[] _gridArray;
        public readonly float CellSize;
        private readonly Vector2 _halfCell;
        private readonly Vector2 _worldOffset;

        public Grid(int width, int height, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, float cellSize = 1.0f)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _halfCell = new Vector2(CellSize / 2.0f, CellSize / 2.0f);

            _gridArray = new TGridObject[width * height];

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

        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return _gridArray[ToArrayIndex(x, y)];
            }

            return default(TGridObject);
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * CellSize + _halfCell;
            return (new Vector2(x, y) - new Vector2(Width / 2f, Height / 2f)) * CellSize + _worldOffset;
        }

        public (int, int) GetGridPosition(Vector2 worldPosition)
        {
            var vec = (worldPosition - _halfCell) / CellSize;

            return ((int) vec.x, (int) vec.y);
        }

        private int ToArrayIndex(int gridX, int gridY)
        {
            var x = gridX + Width / 2;
            var y = Height / 2 - gridY - 1;

            return y * Width + x;
        }
    }
}
