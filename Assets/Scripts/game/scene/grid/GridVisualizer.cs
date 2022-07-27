using UnityEngine;

namespace game.scene.grid
{
    public class GridVisualizer<T> where T : class
    {
        private readonly Grid<T> _grid;

        public GridVisualizer(Grid<T> grid)
        {
            _grid = grid;
        }

        public void Show()
        {
            var halfCellSize = new Vector2(_grid.CellSize, _grid.CellSize) / 2;
            for (var x = 0; x < _grid.Width; x++)
            {
                for (var y = 0; y < _grid.Height; y++)
                {
                    var pos = _grid.GetWorldPosition(x, y) - halfCellSize;
                    var right = _grid.GetWorldPosition(x + 1, y) - halfCellSize;
                    var bottom = _grid.GetWorldPosition(x, y + 1) - halfCellSize;
                    Utilities.CreateWorldText(0.ToString(), null, _grid.GetWorldPosition(x, y), 5, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(pos, bottom, Color.blue, 100f);
                    Debug.DrawLine(pos, right, Color.blue, 100f);
                }
            }

            var bottomLeft = _grid.GetWorldPosition(0, _grid.Height) - halfCellSize;
            var bottomRight = _grid.GetWorldPosition(_grid.Width, _grid.Height) - halfCellSize;
            var topLeft = _grid.GetWorldPosition(_grid.Width, 0) - halfCellSize;
            var topRight = _grid.GetWorldPosition(_grid.Width, _grid.Height) - halfCellSize;
            Debug.DrawLine(bottomLeft, bottomRight, Color.blue, 100f);
            Debug.DrawLine(topLeft, topRight, Color.blue, 100f);
        }
    }
}
