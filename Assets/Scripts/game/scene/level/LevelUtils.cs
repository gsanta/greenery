using game.scene.grid;
using UnityEngine;

namespace game.scene.level
{
    public class LevelUtils
    {
        private GridSystem _gridSystem;

        public LevelUtils(GridSystem gridSystem)
        {
            _gridSystem = gridSystem;
        }

        public Vector2Int GetRandomGrid()
        {
            var width = _gridSystem.Grid.Width;
            var height = _gridSystem.Grid.Height;

            var posX = Random.Range(0, width - 1);
            var posY = Random.Range(0, height - 1);

            return new Vector2Int(posX, posY);
        }
    }
}