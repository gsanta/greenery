using System;
using game.character.characters.enemy;
using game.scene.grid;
using game.scene.tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemapGround;
        
        public GridSystem GridSystem { get; private set; }
        
        public LevelUtils LevelUtils { get; private set; }
        
        private Vector2Int _offset;

        private EnemySpawner _enemySpawner;
        
        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }
        
        public Vector3 TopLeft()
        {
            return TilemapUtils.TopLeft(tilemapGround);
        }

        public Vector3 BottomRight()
        {
            return TilemapUtils.BottomRight(tilemapGround);
        }

        public float CellSize()
        {
            return TilemapUtils.CellSize(tilemapGround);
        }

        private void Awake()
        {
            var minBounds = tilemapGround.localBounds.min;
            _offset = new Vector2Int((int) minBounds.x, (int) minBounds.y);

            GridSystem = new GridSystem(this);
            LevelUtils = new LevelUtils(GridSystem);
        }

        private void Start()
        {
            _enemySpawner.Spawn(this);
        }

        public bool IsWalkable(Vector2Int pos)
        {
            var tile = tilemapGround.GetTile(new Vector3Int(pos.x + _offset.x, pos.y + _offset.y, 0));
            return TileNameMapper.IsWalkableTile(tile != null ? tile.name : null);
        }
    }
}