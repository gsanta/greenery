using game.character.characters.enemy;
using game.character.characters.player;
using game.scene;
using game.scene.area;
using game.scene.level;
using GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game
{
    public class GameManager : MonoBehaviour
    {

        private PlayerFactory _playerFactory;

        private FollowCamera _followCamera;

        private PanelManager _panelManager;

        private PlayerStore _playerStore;

        private bool _isGameStarted;

        public void Construct(PlayerStore playerStore, PlayerFactory playerFactory, FollowCamera followCamera, PanelManager panelManager)
        {
            _playerStore = playerStore;
            _playerFactory = playerFactory;
            _followCamera = followCamera;
            _panelManager = panelManager;
        }

        public void StartLevel(Level level)
        {
            var player = _playerFactory.Create(level.Grid.Graph.GetRandomWorldPosition());
            var player2 = _playerFactory.Create(level.Grid.Graph.GetRandomWorldPosition());
            player2.IsActive = true;
            _followCamera.SetTarget(player);
            _isGameStarted = true;

            _playerStore.GetActivePlayer().IsActive = true;
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
            // _panelManager.startGamePanel.gameObject.SetActive(true);
            SceneManager.LoadScene("Menu");
            // _isGameStarted = false;
            
        }
    }
}