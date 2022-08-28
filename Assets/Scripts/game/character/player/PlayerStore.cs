using game.character.player;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerStore : MonoBehaviour
    {
        private readonly List<Player> _players = new();

        private Dictionary<PlayerType, PlayerStats> _stats = new Dictionary<PlayerType, PlayerStats>();

        public PlayerStore()
        {
            var catStat = new PlayerStats(3);
            catStat.Bullets = 8;
            _stats.Add(PlayerType.Cat, catStat);

            var cowStat = new PlayerStats(5);
            cowStat.Bullets = 3;
            _stats.Add(PlayerType.Cow, catStat);
        }

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

        public PlayerStats GetStat(PlayerType type)
        {
            return _stats[type];
        }
    }
}