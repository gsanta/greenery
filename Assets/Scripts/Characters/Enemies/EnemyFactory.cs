using Items.Bullet;
using Players;
using UnityEngine;

namespace Characters.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private float spawnTime = 5f;

        [SerializeField] private bool isDisabled;

        public Enemy enemyPrefab;

        public Transform spawnPosition;

        private float _timer;

        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private BulletFactory _bulletFactory;

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, BulletFactory bulletFactory)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _bulletFactory = bulletFactory;
        }

        private void Start()
        {
            _timer = spawnTime;
        }

        private void Update()
        {
            if (isDisabled)
            {
                return;
            }

            _timer -= Time.deltaTime;
            if (!(_timer <= 0f)) return;

            var enemy = Instantiate(enemyPrefab, spawnPosition.position, transform.rotation);
            enemy.Construct(_playerStore);
            enemy.GetComponent<Shooting>().Construct(enemy, _bulletFactory, _playerStore);
            _enemyStore.Add(enemy);
            _timer = spawnTime;
        }
    }
}