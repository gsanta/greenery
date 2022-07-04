using System.Collections.Generic;

namespace Characters.Players
{
    public class PlayerStore : ICharacterStore<ICharacter>
    {
        private readonly List<Player> _players = new();

        public List<ICharacter> GetAll()
        {
            return _players.ConvertAll<ICharacter>(player => player);
        }


        public void Add(ICharacter player)
        {
            _players.Add((Player) player);
        }

        public void Remove(ICharacter player)
        {
            _players.Remove((Player) player);
        }

        public Player GetActivePlayer()
        {
            return _players[0];
        }
    }
}