using System.Collections.Generic;
using Characters.Enemies;

namespace Characters
{
    public interface ICharacterStore<T> where T : ICharacter
    {
        public List<T> GetAll();

        public void Add(T enemy);

        public void Remove(T enemy);
    }
}