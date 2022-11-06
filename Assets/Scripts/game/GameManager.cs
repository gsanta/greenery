using game.character.characters.player;
using game.character.movement;
using game.GamePlay;
using game.scene;
using game.scene.level;
using Game.Stage;
using GUI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game
{
    public class GameManager : MonoBehaviour
    {
        private PlayerManager _playerManager;

        private PanelManager _panelManager;

        private FollowCamera _followCamera;

        private LevelLoader _levelLoader;

        private StageManager _stageManager;

        private MovementManager _movementManager;

        private EnemyManager _enemyManager;

        private bool _isGameStarted;

        public void Construct(PlayerManager playerManager, PanelManager panelManager, StageManager stageManager, FollowCamera followCamera, LevelLoader levelLoader, MovementManager movementManager, EnemyManager enemyManager)
        {
            _playerManager = playerManager;
            _panelManager = panelManager;
            _followCamera = followCamera;
            _levelLoader = levelLoader;
            _stageManager = stageManager;
            _movementManager = movementManager;
            _enemyManager = enemyManager;
            _levelLoader.LevelsStartedEventHandler += HandleLevelsStarted;
        }

        private void HandleLevelsStarted(object sender, EventArgs args)
        {
            _playerManager.Activate();
            _enemyManager.Activate();
            _movementManager.Activate();
            _stageManager.Init();
            _followCamera.Init();
            _isGameStarted = true;
        }

        public bool IsGameStarted()
        {
            return _isGameStarted;
        }

        private void Start()
        {
            _panelManager.startGamePanel.gameObject.SetActive(false);
        }

        public void EndGame()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}