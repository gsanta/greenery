
using game.scene.tile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.level
{
    public class Environment
    {
        private Tilemap _tilemapGround;

        private Transform _blocksContainer;

        private List<Transform> blocks = new();

        private Vector2Int _offset;

        public Vector3 TopLeft { get; private set; }

        public Vector3 BottomRight { get; private set; }

        public Vector2 Center { get; private set; }

        public float CellSize { get; private set; }

        public Environment(Transform blocksContainer, Tilemap tilemapGround)
        {
            _blocksContainer = blocksContainer;
            _tilemapGround = tilemapGround;
        }

        public bool IsWalkable(Vector2Int pos)
        {
            var tile = _tilemapGround.GetTile(new Vector3Int(pos.x + _offset.x, pos.y + _offset.y, 0));
            return TileNameMapper.IsWalkableTile(tile != null ? tile.name : null);
        }

        public void Init()
        {
            foreach (Transform child in _blocksContainer)
            {
                blocks.Add(child);
            }

            var minBounds = _tilemapGround.localBounds.min;
            _offset = new Vector2Int((int)minBounds.x, (int)minBounds.y);

            TopLeft = TilemapUtils.TopLeft(_tilemapGround);
            BottomRight = TilemapUtils.BottomRight(_tilemapGround);
            CellSize = TilemapUtils.CellSize(_tilemapGround);
            Center = new Vector2((TopLeft.x + BottomRight.x) / 2, (TopLeft.y + BottomRight.y) / 2);
        }
    }
}
