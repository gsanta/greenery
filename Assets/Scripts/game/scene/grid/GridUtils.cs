using UnityEngine;

namespace game.scene.grid
{
    public class GridUtils
    {

        public static Vector2 GetRandomPosition<T>(Grid<T> grid) where T : class
        {
            var x = Random.Range(0, grid.Width);
            var y = Random.Range(0, grid.Height);

            return grid.GetWorldPosition(x, y);
        }
    }
}