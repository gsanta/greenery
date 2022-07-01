using System.Collections.Generic;

namespace Characters.Enemies
{
    public class EnemyStore
    {
        private readonly List<Enemy> _enemies = new();

        public List<Enemy> GetEnemies()
        {
            return _enemies;
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}