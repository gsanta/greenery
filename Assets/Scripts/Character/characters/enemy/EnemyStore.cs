using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Character.characters.enemy
{
    public class EnemyStore : MonoBehaviour, ICharacterStore<ICharacter>
    {
        private readonly List<Enemy> _enemies = new();

        public List<ICharacter> GetAll()
        {
            return _enemies.ConvertAll<ICharacter>(enemy => enemy);
        }

        public void Add(ICharacter enemy)
        {
            _enemies.Add((Enemy) enemy);
        }
        
        public void Remove(ICharacter enemy)
        {
            _enemies.Remove((Enemy) enemy);
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