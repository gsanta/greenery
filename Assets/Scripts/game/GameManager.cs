using game.character.characters.player;
using game.character.enemy;
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

        private bool _isGameStarted;

        public void Construct(PlayerManager playerManager, PanelManager panelManager, StageManager stageManager, FollowCamera followCamera, LevelLoader levelLoader)
        {
            _playerManager = playerManager;
            _panelManager = panelManager;
            _followCamera = followCamera;
            _levelLoader = levelLoader;
            _stageManager = stageManager;
            _levelLoader.LevelsStartedEventHandler += HandleLevelsStarted;
        }

        private void HandleLevelsStarted(object sender, EventArgs args)
        {
            _stageManager.Init();
            _playerManager.Activate();
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