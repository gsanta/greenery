using game.scene.level;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyFactory _enemyFactory;

        private EnemyStore _enemyStore;

        private LevelStore _levelStore;

        public bool IsActive { set; private get; }

        public void Construct(EnemyFactory enemyFactory, EnemyStore enemyStore, LevelStore levelStore)
        {
            _enemyFactory = enemyFactory;
            _enemyStore = enemyStore;
            _levelStore = levelStore;
        }
        
        private void Update()
        {
            if (!IsActive)
            {
                return;
            }

            if (_enemyStore.Count() == 0)
            {
                _enemyFactory.Create(_levelStore.Level);
            }
        }
    }
}