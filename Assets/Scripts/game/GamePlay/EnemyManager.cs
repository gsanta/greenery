using game.character.characters.enemy;

namespace Assets.Scripts.game.GamePlay
{
    public class EnemyManager
    {
        private EnemySpawner _enemySpawner;

        public EnemyManager(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        public void Activate()
        {
            _enemySpawner.IsDisabled = false;
        }
    }
}
