using game.scene.grid;
using UnityEngine;

namespace game.scene.level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] public LevelName levelName;

        private GameObject _border;

        public LevelBounds LevelBounds { get; set; }

        public Environment Environment { get; set; }

        public GridGraph<PathNode> Graph { get; set; }

        public GameObject RootGameObject { get; set; }

        private GridFactory _gridFactory;

        private LevelLoader _levelLoader;

        private GameManager _gameManager;

        private GridVisualizer _gridVisualizer;

        public void Construct(GameObject border, LevelLoader levelLoader, GameManager gameManager, GridVisualizer gridVisualizer)
        {
            _border = border;
            _levelLoader = levelLoader;
            _gameManager = gameManager;
            _gridVisualizer = gridVisualizer;

            _gridFactory = new GridFactory();

            _levelLoader.AddLevel(this);
        }

        private void Start()
        {
            LevelBounds.Init(_border);
            Environment.Init();

            Graph = _gridFactory.CreateGrid(LevelBounds, Environment);
            _gridVisualizer.Construct(Graph);

            _gameManager.StartLevel(this);
        }
    }
}