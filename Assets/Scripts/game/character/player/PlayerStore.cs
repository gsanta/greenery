using game.character.player;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerStore : MonoBehaviour
    {
        private readonly List<Player> _players = new();

        private Dictionary<CharacterType, PlayerStats> _stats = new Dictionary<CharacterType, PlayerStats>();

        public PlayerStore()
        {
            var catStat = new PlayerStats(3);
            catStat.Bullets = 8;
            _stats.Add(CharacterType.Cat, catStat);

            var cowStat = new PlayerStats(5);
            cowStat.Bullets = 3;
            _stats.Add(CharacterType.Cow, catStat);
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
            return _players.Count > 0 ? _players[0] : null;
        }

        public void DestroyAll()
        {
            foreach (var player in _players)
            {
                Destroy(player);
            }
        }

        public PlayerStats GetStat(CharacterType type)
        {
            return _stats[type];
        }
    }
}