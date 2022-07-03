using System.Collections.Generic;

namespace Characters.Enemies
{
    public class EnemyStore : ICharacterStore<ICharacter>
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
    }
}