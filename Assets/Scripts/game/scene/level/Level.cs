using game.scene.grid;
using game.scene.tile;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace game.scene.level
{
    public class LevelStartedEventArgs : EventArgs
    {
        public Level Level { get; set; }
    }

    public class Level : MonoBehaviour
    {
        [SerializeField] public LevelName levelName;

        public event EventHandler<LevelStartedEventArgs> LevelStartedEventHandler;

        public EnvironmentData EnvironmentData { get; set; } 

        public GridGraph Grid { get; set; }

        public GameObject RootGameObject { get; set; }

        private GridFactory _gridFactory;

        private GameManager _gameManager;

        public TilemapHandler TilemapHandler { get; set; } 

        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void HandleLevelStarted()
        {
            EventHandler<LevelStartedEventArgs> handler = LevelStartedEventHandler;
            if (handler != null)
            {
                var eventArgs = new LevelStartedEventArgs();
                eventArgs.Level = this;
                handler(this, eventArgs);
            }
        }

        private void Start()
        {
            EnvironmentData.Init();

            _gridFactory = new GridFactory(EnvironmentData);

            Grid = _gridFactory.CreateGrid();

            _gameManager.StartLevel(this);

            HandleLevelStarted();
        }
    }
}