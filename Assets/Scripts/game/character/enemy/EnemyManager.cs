
using game.character.characters.enemy;
using game.scene.level;

namespace game.character.enemy
{
    public class EnemyManager
    {
        private EnemyFactory _enemyFactory;

        private EnemySpawner _enemySpawner;

        public EnemyManager(EnemyFactory enemyFactory, EnemySpawner enemySpawner)
        {
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
        }

        public void Start(Level level)
        {
            var enemy = _enemyFactory.Create(level);
            _enemySpawner.IsActive = true;
        }
    }
}