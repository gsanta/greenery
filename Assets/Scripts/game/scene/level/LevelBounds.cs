using game.scene.tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.level
{
    public class LevelBounds
    {
        private Tilemap _tilemapGround;

        public Vector3 TopLeft { get; private set; }

        public Vector3 BottomRight { get; private set; }

        public Vector2 Center { get; private set; }

        public float CellSize { get; private set; }

        public LevelBounds(Tilemap tilemapGround)
        {
            _tilemapGround = tilemapGround;
        }


        public void Init()
        {
            TopLeft = TilemapUtils.TopLeft(_tilemapGround);
            BottomRight = TilemapUtils.BottomRight(_tilemapGround);
            CellSize = TilemapUtils.CellSize(_tilemapGround);
            Center = new Vector2((TopLeft.x + BottomRight.x) / 2, (TopLeft.y + BottomRight.y) / 2);
        }
    }
}
