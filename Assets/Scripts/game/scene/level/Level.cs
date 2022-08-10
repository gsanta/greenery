using game.character.characters.enemy;
using game.scene.grid;
using UnityEngine;

namespace game.scene.level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] public LevelName levelName;

        public LevelBounds LevelBounds { get; set; }

        public Environment Environment { get; set; }

        public grid.Grid Grid { get; set; }
        
        private EnemyFactory _enemyFactory;

        private GridVisualizer _gridVisualizer;

        private LevelLoader _levelLoader;

        private GameManager _gameManager;

        public void Construct(EnemyFactory enemyFactory, LevelLoader levelLoader, GridVisualizer gridVisualizer, GameManager gameManager)
        {
            _enemyFactory = enemyFactory;
            _levelLoader = levelLoader;
            _gridVisualizer = gridVisualizer;
            _gameManager = gameManager;

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
            LevelBounds.Init();
            Environment.Init();
            Grid.Init();
            _gridVisualizer.GridSystem = Grid;

            if (true)
            {
                _gridVisualizer.Show();
            }

            _enemyFactory.Create(this);
            _gameManager.StartLevel(this);
        }
    }
}