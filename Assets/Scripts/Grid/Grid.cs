using System;
using UnityEngine;

namespace Grid
{
    public class Grid<TGridObject>
    {
        public readonly int Width;
        public readonly int Height;
        private readonly TGridObject[,] _gridArray;
        public readonly float CellSize;
        private readonly int _offsetWidth;
        private readonly int _offsetHeight;

        public Grid(int width, int height, float cellSize, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, int offsetWidth = 0, int offsetHeight = 0)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            _offsetWidth = offsetWidth;
            _offsetHeight = offsetHeight;

            _gridArray = new TGridObject[width, height];

            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    _gridArray[x, y] = createGridObject(this, x, y);
                }
            }
        }

        public bool IsWithinGrid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public void SetValue(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                _gridArray[x, y] = value;
            }
        }

        public TGridObject[] GetColumnObjects(int x)
        {
            TGridObject[] row = new TGridObject[Width];

            for (int i = 0; i < Width; i++)
            {
                row[i] = _gridArray[x, i];
            }

            return row;
        }

        public TGridObject[] GetRowObjects(int y)
        {
            TGridObject[] columns = new TGridObject[Height];

            for (int i = 0; i < Height; i++)
            {
                columns[i] = _gridArray[i, y];
            }

            return columns;
        }

        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                return _gridArray[x, y];
            }

            return default(TGridObject);
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return (new Vector3(x, y) - new Vector3(Width / 2f, Height / 2f) + new Vector3(_offsetWidth, _offsetHeight)) *
                   CellSize;
        }
    }
}
