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
                Utilities.CreateWorldText(0.ToString(), null, grid.GetWorldPosition(x, y), 20, Color.white, TextAnchor.MiddleCenter);
            }
        }
    }
}
