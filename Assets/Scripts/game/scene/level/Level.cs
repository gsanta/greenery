using game.character.characters.enemy;
using game.character.characters.player;
using game.scene.grid;
using UnityEngine;

namespace game.scene.level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] public LevelName levelName;

        public Environment Environment;

        public GridSystem GridSystem { get; private set; }
        
        public LevelUtils LevelUtils { get; private set; }
        
        private EnemyFactory _enemyFactory;

        private PlayerStore _playerStore;

        private GridVisualizer _gridVisualizer;

        private LevelLoader _levelLoader;

        public void Construct(EnemyFactory enemyFactory, PlayerStore playerStore, LevelLoader levelLoader, GridVisualizer gridVisualizer, Environment environment)
        {
            _enemyFactory = enemyFactory;
            _playerStore = playerStore;
            _levelLoader = levelLoader;
            _gridVisualizer = gridVisualizer;
            Environment = environment;

            _levelLoader.AddLevel(this);
        }

        //public Direction GetQuarter(Vector2 worldPos)
        //{
        //    if (worldPos.x > Center.x)
        //    {
        //        return worldPos.y > Center.y ? Direction.RightUp : Direction.RightDown;
        //    }
            
        //    return worldPos.y > Center.y ? Direction.LeftUp : Direction.LeftDown;
        //}

        private void Start()
        {
            Environment.Init();
            GridSystem = new GridSystem(this);
            _gridVisualizer.GridSystem = GridSystem;

            LevelUtils = new LevelUtils(GridSystem);

            if (true)
            {
                _gridVisualizer.Show();
            }

            _enemyFactory.Create(this);
        }
    }
}