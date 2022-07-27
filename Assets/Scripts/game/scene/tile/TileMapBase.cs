using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.tile
{
    public class TileMapBase : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        public bool IsWalkable(Vector2Int pos)
        {
            var tile = _tilemap.GetTile(new Vector3Int(pos.x, pos.y, 0));
            return true;
        }
    }
}