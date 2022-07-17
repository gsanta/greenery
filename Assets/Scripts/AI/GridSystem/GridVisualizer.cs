using UnityEngine;

namespace AI.GridSystem
{
    public class GridVisualizer<T> where T : class
    {
        private Grid<T> grid;

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
                    Utilities.CreateWorldText(0.ToString(), null, grid.GetWorldPosition(x, y) + new Vector3(grid.CellSize, grid.CellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }

            Debug.DrawLine(grid.GetWorldPosition(0, grid.Height), grid.GetWorldPosition(grid.Width, grid.Height), Color.white, 100f);
            Debug.DrawLine(grid.GetWorldPosition(grid.Width, 0), grid.GetWorldPosition(grid.Width, grid.Height), Color.white, 100f);
        }
    }
}
