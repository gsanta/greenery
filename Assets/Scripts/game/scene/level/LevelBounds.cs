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

        public Vector2 Size { get; private set; }

        public float CellSize { get; private set; }

        public LevelBounds(Tilemap tilemapGround)
        {
            _tilemapGround = tilemapGround;
        }


        public void Init(GameObject border)
        {
            var bounds = border.GetComponent<Renderer>().bounds;


            TopLeft = new Vector2(bounds.min.x, bounds.max.y);
            BottomRight = new Vector2(bounds.max.x, bounds.min.y);
            CellSize = 1;
            Center = new Vector2((TopLeft.x + BottomRight.x) / 2, (TopLeft.y + BottomRight.y) / 2);

            Size = new Vector2(bounds.size.x, bounds.size.y);
        }
    }
}
