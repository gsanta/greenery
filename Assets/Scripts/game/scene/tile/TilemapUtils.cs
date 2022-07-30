using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.tile
{
    public static class TilemapUtils
    {
        public static Vector3 TopLeft(Tilemap tilemap)
        {
            var localBounds = tilemap.localBounds;
            var minBounds = localBounds.min;
            var maxBounds = localBounds.max;
            var pos = tilemap.GetCellCenterWorld(new Vector3Int((int) minBounds.x, (int)maxBounds.y, 0));
            var cellSize = CellSize(tilemap);
            return new Vector3(pos.x - cellSize, pos.y + cellSize);
        }

        public static Vector3 BottomRight(Tilemap tilemap)
        {
            var localBounds = tilemap.localBounds;
            var minBounds = localBounds.min;
            var maxBounds = localBounds.max;
            var pos = tilemap.GetCellCenterWorld(new Vector3Int((int) maxBounds.x, (int) minBounds.y, 0));
            var cellSize = CellSize(tilemap);
            return new Vector3(pos.x + cellSize, pos.y - cellSize);
        }

        public static float CellSize(Tilemap tilemap)
        {
            var localBounds = tilemap.localBounds;
            var minBounds = localBounds.min;
            var maxBounds = localBounds.max;
            var pos = tilemap.GetCellCenterWorld(new Vector3Int((int) minBounds.x, (int) minBounds.y, 0));
            var pos2 = tilemap.GetCellCenterWorld(new Vector3Int((int) minBounds.x + 1, (int) minBounds.y, 0));

            return pos2.x - pos.x;
        }
    }
}