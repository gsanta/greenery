using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.tile
{
    public class TileMapBase : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemapGround;
        // [SerializeField] private Tilemap _tilemapWater;

        private Vector2Int _offset;
        
        private void Awake()
        {
            var minBounds = _tilemapGround.localBounds.min;
            _offset = new Vector2Int((int) minBounds.x, (int) minBounds.y);
        }

        public bool IsWalkable(Vector2Int pos)
        {
            var tile = _tilemapGround.GetTile(new Vector3Int(pos.x + _offset.x, pos.y + _offset.y, 0));
            return TileNameMapper.IsWalkableTile(tile != null ? tile.name : null);
        }
    }
}