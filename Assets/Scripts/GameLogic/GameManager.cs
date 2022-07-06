using Characters.Enemies;
using Characters.Players;
using GUI;
using Items.EnterArea;
using Players;
using Scene;
using UnityEngine;

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
        
        public void StartGame()
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
            _panelManager.startGamePanel.gameObject.SetActive(true);
            _isGameStarted = false;
        }
    }
}