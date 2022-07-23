using AI.GridSystem;
using AI.pathFinding;
using AI.Player;
using Characters.Common;
using Characters.Players;
using GameLogic;
using Items.Bullet;
using UnityEngine;

namespace Characters.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        public Enemy enemyPrefab;

        public Transform spawnPosition;

        private float _timer;

        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private BulletFactory _bulletFactory;

        private GameManager _gameManager;

        private GridComponent _gridComponent;

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, BulletFactory bulletFactory, GameManager gameManager, GridComponent gridComponent)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _bulletFactory = bulletFactory;
            _gameManager = gameManager;
            _gridComponent = gridComponent;
        }

        public void Create()
        {
            var enemy = Instantiate(enemyPrefab, spawnPosition.position, transform.rotation);
            enemy.Construct(_enemyStore, _playerStore, _gameManager);
            enemy.GetComponent<Shooting>().Construct(enemy, _bulletFactory, _playerStore);
            enemy.GetComponent<Health>().Construct(enemy, 100, null);
            var pathMovement = enemy.GetComponent<PathMovement>();
            pathMovement.Construct(_gridComponent.GetPathFinding(), _gridComponent);

            var roamingState = new RoamingState(_gridComponent.GetGrid(), enemy, pathMovement);
            enemy.SetRoamingState(roamingState);

            
            _enemyStore.Add(enemy);
        }
    }
}