using Players;
using UnityEngine;

namespace Characters.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnTime = 5f;
        
        [SerializeField] private bool isDisabled = false;
        
        public Enemy enemyPrefab;
        
        public Transform player;
        
        public Transform spawnPosition;
        
        private float _timer;
        
        private PlayerStore _playerStore;
        
        private EnemyStore _enemyStore;
        
        public void Construct(EnemyStore enemyStore, PlayerStore playerStore)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
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
            _enemyStore.AddEnemy(enemy);        
            _timer = spawnTime;
        }
    }
}
