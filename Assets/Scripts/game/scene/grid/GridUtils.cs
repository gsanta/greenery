using UnityEngine;

namespace game.scene.grid
{
    public class GridUtils
    {

        public static Vector2 GetRandomPosition<T>(GridGraph<T> grid) where T : class
        {
            var x = Random.Range(0, grid.Width);
            var y = Random.Range(0, grid.Height);

            return grid.GetWorldPosition(x, y);
        }

        public static Vector2Int GetRandomGrid<T>(GridGraph<T> grid) where T : class
        {
            var width = grid.Width;
            var height = grid.Height;

            var posX = Random.Range(0, width - 1);
            var posY = Random.Range(0, height - 1);

            return new Vector2Int(posX, posY);
        }
    }
}