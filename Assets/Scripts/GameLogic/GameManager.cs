using Characters.Enemies;
using Items.EnterArea;
using Players;
using UnityEngine;

namespace GameLogic
{
    public class GameManager : MonoBehaviour
    {

        private EnterAreaStore _enterAreaStore;

        private PlayerFactory _playerFactory;

        private EnemySpawner _enemySpawner;

        public void Construct(EnterAreaStore enterAreaStore, PlayerFactory playerFactory, EnemySpawner enemySpawner)
        {
            _enterAreaStore = enterAreaStore;
            _playerFactory = playerFactory;
            _enemySpawner = enemySpawner;
        }
        
        public void StartGame()
        {
            var enterArea = _enterAreaStore.ChooseEnterArea();
            _playerFactory.Create(enterArea.transform.position);
            _enemySpawner.SetEnabled(true);
        }

        public void EndGame()
        {
            
        }
    }
}