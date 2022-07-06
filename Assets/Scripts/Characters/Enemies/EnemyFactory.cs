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

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, BulletFactory bulletFactory, GameManager gameManager)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _bulletFactory = bulletFactory;
            _gameManager = gameManager;
        }

        public void Create()
        {
            var enemy = Instantiate(enemyPrefab, spawnPosition.position, transform.rotation);
            enemy.Construct(_enemyStore, _playerStore, _gameManager);
            enemy.GetComponent<Shooting>().Construct(enemy, _bulletFactory, _playerStore);
            enemy.GetComponent<Health>().Construct(enemy, 100, null);
            _enemyStore.Add(enemy);
        }
    }
}