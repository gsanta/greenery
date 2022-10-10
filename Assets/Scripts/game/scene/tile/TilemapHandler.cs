
using game.scene.grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.tile
{
    public class TilemapHandler : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;

        public void GetTileAt(Vector2 pos)
        {
            GridLayout gridLayout = tilemap.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(pos);
    
        }
    }
}
