using game.character.characters.player;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemyStore : MonoBehaviour, ICharacterStore<Enemy>
    {
        private readonly List<Enemy> _enemies = new();

        private Enemy _currentEnemy;

        public List<Enemy> GetAll()
        {
            return _enemies;
        }

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }
        
        public void Remove(Enemy enemy)
        {
            _enemies.Remove((Enemy) enemy);
        }

        public Enemy GetCurrentEnemy()
        {
            return _currentEnemy;
        }

        public Enemy SetNextEnemy()
        {
            if (!_currentEnemy)
            {
                _currentEnemy = _enemies[0];
               
            } else
            {
                var index = _enemies.IndexOf(_currentEnemy);

                if (index == _enemies.Count - 1)
                {
                    _currentEnemy = _enemies[0];
                }
                else
                {
                    _currentEnemy = _enemies[index + 1];
                }
            }
            
            return _currentEnemy;
        }

        public int Count()
        {
            return _enemies.Count;
        }

        public void DestroyAll()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy);
            }
        }
    }
}