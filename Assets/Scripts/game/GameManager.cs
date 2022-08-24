using game.character.characters.player;
using game.character.enemy;
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

        private EnemyManager _enemyManager;

        private bool _isGameStarted;

        public void Construct(PlayerManager playerManager, PanelManager panelManager, EnemyManager enemyManager)
        {
            _playerManager = playerManager;
            _enemyManager = enemyManager;
            _panelManager = panelManager;
        }

        public void StartLevel(Level level)
        {
            _playerManager.Start(level);
            _enemyManager.Start(level);
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