using UnityEngine;

namespace AI.GridSystem
{
    public class GridUtils
    {

        public static Vector2 GetRandomPosition<T>(Grid<T> grid) where T : class
        {
            var x = Random.Range(grid.MinX, grid.MaxX);
            var y = Random.Range(grid.MinY, grid.MaxY);

            return grid.GetWorldPosition(x, y);
        }
    }
}