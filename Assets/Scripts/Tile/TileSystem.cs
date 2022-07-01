using System;
using Players;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tile
{
    public class TileSystem : MonoBehaviour
    {
        private Tilemap _tilemap;
        private PlayerStore _playerStore;

        public void Construct(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        private void Start()
        {
            _tilemap = GetComponent<Tilemap>();
            BoundsInt bounds = _tilemap.cellBounds;
            TileBase[] tiles = _tilemap.GetTilesBlock(bounds);
            Debug.Log("hello");

            InvokeRepeating("CurrentTile", 1.0f, 0.1f);
        }

        private void CurrentTile()
        {
            var cellPosition = _tilemap.WorldToCell(_playerStore.GetActivePlayer().transform.position);
            var cell = _tilemap.GetTile(cellPosition);

            if (cell != null)
            {
                Debug.Log(("cell name is + " + cell.name));
            }
        }
    }
}