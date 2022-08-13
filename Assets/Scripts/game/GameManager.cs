using game.character.characters.player;
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


        private bool _isGameStarted;

        public void Construct(PlayerManager playerManager, PanelManager panelManager)
        {
            _playerManager = playerManager;
            _panelManager = panelManager;
        }

        public void StartLevel(Level level)
        {
            _playerManager.Start(level);
            _isGameStarted = true;
        }

        public bool IsGameStarted()
        {
            return _isGameStarted;
        }

        private void Update()
        {
            _playerManager.Update();
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