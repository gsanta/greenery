using game.scene.grid;
using UnityEngine;

namespace game.scene.level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] public LevelName levelName;

        public EnvironmentData EnvironmentData { get; set; } 

        public GridGraph<PathNode> Grid { get; set; }

        public GameObject RootGameObject { get; set; }

        private GridFactory _gridFactory;

        private GameManager _gameManager;

        public GridVisualizer gridVisualizer;

        public void Construct(GameManager gameManager, GridVisualizer gridVisualizer)
        {
            _gameManager = gameManager;
            this.gridVisualizer = gridVisualizer;
        }

        private void Start()
        {
            EnvironmentData.Init();

            _gridFactory = new GridFactory(EnvironmentData);

            Grid = _gridFactory.CreateGrid();

            gridVisualizer.Construct(Grid);

            _gameManager.StartLevel(this);
        }
    }
}