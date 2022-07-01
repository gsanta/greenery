using System.Collections.Generic;

namespace Players
{
    public class PlayerStore
    {
        private readonly List<Player> _players = new();

        public List<Player> GetPlayers()
        {
            return _players;
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            _players.Remove(player);
        }

        public Player GetActivePlayer()
        {
            return _players[0];
        }
    }
}