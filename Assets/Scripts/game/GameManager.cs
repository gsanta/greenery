using game.character.characters.player;
using game.character.enemy;
using game.scene;
using game.scene.level;
using GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game
{
    public class GameManager : MonoBehaviour
    {
        private PlayerManager _playerManager;

        private PanelManager _panelManager;

        private FollowCamera _followCamera;

        private bool _isGameStarted;

        public void Construct(PlayerManager playerManager, PanelManager panelManager, FollowCamera followCamera)
        {
            _playerManager = playerManager;
            _panelManager = panelManager;
            _followCamera = followCamera;
        }

        public void StartLevel(Level level)
        {
            _playerManager.Start(level);
            _followCamera.GetConfiner().SetDimensions(level);
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