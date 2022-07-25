using UnityEngine;

namespace AI.grid
{
    public class GridVisualizer<T> where T : class
    {
        private Grid<T> grid;
        private float _zWorldPos = 1.0f;

        public GridVisualizer(Grid<T> grid)
        {
            this.grid = grid;
        }

        public void Show()
        {
            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    Utilities.CreateWorldText(0.ToString(), null, grid.GetWorldPosition(x, y, _zWorldPos), 5, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y + 1, _zWorldPos), Color.red, 100f);
                    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x + 1, y, _zWorldPos), Color.red, 100f);
                }
            }

            Debug.DrawLine(grid.GetWorldPosition(0, grid.Height, _zWorldPos), grid.GetWorldPosition(grid.Width, grid.Height, _zWorldPos), Color.red, 100f);
            Debug.DrawLine(grid.GetWorldPosition(grid.Width, 0, _zWorldPos), grid.GetWorldPosition(grid.Width, grid.Height, _zWorldPos), Color.red, 100f);
        }
    }
}
