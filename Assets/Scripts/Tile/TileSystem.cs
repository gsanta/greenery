using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tile
{
    public class TileSystem : MonoBehaviour
    {
        private Tilemap tilemap;
        [SerializeField] private Transform player;
        
        private void Start()
        {
            tilemap = GetComponent<Tilemap>();
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] tiles = tilemap.GetTilesBlock(bounds);
            Debug.Log("hello");

            InvokeRepeating("CurrentTile", 1.0f, 0.1f);
        }

        private void CurrentTile()
        {
            var cellPosition = tilemap.WorldToCell(player.position);
            var cell = tilemap.GetTile(cellPosition);

            if (cell != null)
            {
                Debug.Log(("cell name is + " + cell.name));
            }
        }
    }
}