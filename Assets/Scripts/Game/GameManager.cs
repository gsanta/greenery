using Character.characters.enemy;
using Character.characters.player;
using GUI;
using Items.EnterArea;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {

        private EnterAreaStore _enterAreaStore;

        private PlayerFactory _playerFactory;

        private EnemySpawner _enemySpawner;

        private FollowCamera _followCamera;

        private PanelManager _panelManager;

        private bool _isGameStarted;

        public void Construct(EnterAreaStore enterAreaStore, PlayerFactory playerFactory, EnemySpawner enemySpawner, FollowCamera followCamera, PanelManager panelManager)
        {
            _enterAreaStore = enterAreaStore;
            _playerFactory = playerFactory;
            _enemySpawner = enemySpawner;
            _followCamera = followCamera;
            _panelManager = panelManager;
        }

        public bool IsGameStarted()
        {
            return _isGameStarted;
        }
        
        private void Start()
        {
            var enterArea = _enterAreaStore.ChooseEnterArea();
            var player = _playerFactory.Create(enterArea.transform.position);
            _enemySpawner.SetEnabled(true);
            _followCamera.SetTarget(player);
            _panelManager.startGamePanel.gameObject.SetActive(false);

            _isGameStarted = true;
        }

        public void EndGame()
        {
            // _panelManager.startGamePanel.gameObject.SetActive(true);
            SceneManager.LoadScene("Menu");
            // _isGameStarted = false;
            
        }
    }
}