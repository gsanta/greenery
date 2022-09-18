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

        public grid.Grid Grid { get; set; }
        

        private LevelLoader _levelLoader;

        private GameManager _gameManager;

        public void Construct(GameObject border, LevelLoader levelLoader, GameManager gameManager)
        {
            _border = border;
            _levelLoader = levelLoader;
            _gameManager = gameManager;

            _levelLoader.AddLevel(this);
        }

        private void Start()
        {
            LevelBounds.Init(_border);
            Environment.Init();
            Grid.Init();

            _gameManager.StartLevel(this);
        }
    }
}