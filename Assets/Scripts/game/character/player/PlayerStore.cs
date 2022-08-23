using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerStore : MonoBehaviour
    {
        private readonly List<Player> _players = new();

        public List<Player> GetAll()
        {
            return _players;
        }

        public void Add(Player player)
        {
            _players.Add((Player) player);
        }

        public void DestroyActivePlayer()
        {
            var activePlayer = GetActivePlayer();
            _players.Remove(activePlayer);
            Destroy(activePlayer.gameObject);
        }

        public Player GetActivePlayer()
        {
            return _players[0];
        }

        public void DestroyAll()
        {
            foreach (var player in _players)
            {
                Destroy(player);
            }
        }
    }
}