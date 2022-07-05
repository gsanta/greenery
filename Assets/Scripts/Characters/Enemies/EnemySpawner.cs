using UnityEngine;

namespace Characters.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnTime = 5f;

        private float _timer;

        private bool _isEnabled = false;

        private EnemyFactory _enemyFactory;

        public void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
        }
        
        private void Update()
        {
            if (!_isEnabled)
            {
                return;
            }

            _timer -= Time.deltaTime;
            if (!(_timer <= 0f)) return;
            
            _enemyFactory.Create();
            _timer = spawnTime;
        }
    }
}