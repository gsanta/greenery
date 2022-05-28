using UnityEngine;

public class GridVisualizer<T>
{
    private Grid<T> grid;

    public GridVisualizer(Grid<T> grid)
    {
        this.grid = grid;
    }

    public void Show()
    {
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                Utilities.CreateWorldText(0.ToString(), null, grid.GetWorldPosition(x, y) + new Vector3(grid.cellSize, grid.cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(grid.GetWorldPosition(0, grid.height), grid.GetWorldPosition(grid.width, grid.height), Color.white, 100f);
        Debug.DrawLine(grid.GetWorldPosition(grid.width, 0), grid.GetWorldPosition(grid.width, grid.height), Color.white, 100f);

    }
}
