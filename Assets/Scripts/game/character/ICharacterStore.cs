using System.Collections.Generic;

namespace game.character
{
    public interface ICharacterStore<T> where T : ICharacter
    {
        public List<T> GetAll();

        public void Add(T enemy);

        public void Remove(T enemy);
    }
}