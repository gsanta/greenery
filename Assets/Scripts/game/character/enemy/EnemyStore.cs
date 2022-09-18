using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemyStore : MonoBehaviour, ICharacterStore<Enemy>
    {
        private readonly List<Enemy> _enemies = new();

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