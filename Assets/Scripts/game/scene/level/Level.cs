using game.character.characters.enemy;
using game.character.characters.player;
using game.scene.grid;
using game.scene.tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemapGround;

        [SerializeField] private Vector2Int levelIndex;

        [SerializeField] public LevelName levelName;

        public GridSystem GridSystem { get; private set; }
        
        public LevelUtils LevelUtils { get; private set; }
        
        private Vector2Int _offset;

        private EnemyFactory _enemyFactory;

        private PlayerStore _playerStore;

        public Vector3 TopLeft { get; private set; }
        
        public Vector3 BottomRight { get; private set; }
        
        public Vector2 Center { get; private set; }

        public float CellSize { get; private set; }

        private LevelLoader _levelLoader;

        public void Construct(EnemyFactory enemyFactory, PlayerStore playerStore, LevelLoader levelLoader)
        {
            _enemyFactory = enemyFactory;
            _playerStore = playerStore;
            _levelLoader = levelLoader;
            
            _levelLoader.AddLevel(this);
        }

        public Direction GetQuarter(Vector2 worldPos)
        {
            if (worldPos.x > Center.x)
            {
                return worldPos.y > Center.y ? Direction.RightUp : Direction.RightDown;
            }
            
            return worldPos.y > Center.y ? Direction.LeftUp : Direction.LeftDown;
        }
        
        private void Awake()
        {
            Injector.Instance.InjectLevel(this);
            var minBounds = tilemapGround.localBounds.min;
            _offset = new Vector2Int((int) minBounds.x, (int) minBounds.y);

            TopLeft = TilemapUtils.TopLeft(tilemapGround);
            BottomRight = TilemapUtils.BottomRight(tilemapGround);
            CellSize = TilemapUtils.CellSize(tilemapGround);
            Center = new Vector2((TopLeft.x + BottomRight.x) / 2, (TopLeft.y + BottomRight.y) / 2);
            
            GridSystem = new GridSystem(this);
            LevelUtils = new LevelUtils(GridSystem);
        }

        private void Start()
        {
            _enemyFactory.Create(this);
        }

        public bool IsWalkable(Vector2Int pos)
        {
            var tile = tilemapGround.GetTile(new Vector3Int(pos.x + _offset.x, pos.y + _offset.y, 0));
            return TileNameMapper.IsWalkableTile(tile != null ? tile.name : null);
        }
    }
}