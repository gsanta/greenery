using game.character.player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerStore : MonoBehaviour
    {
        public event EventHandler OnActivePlayerChange;

        private readonly List<Player> _players = new();

        private Dictionary<CharacterType, PlayerStats> _stats = new Dictionary<CharacterType, PlayerStats>();

        private Player _activePlayer;

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

        public void Add(Player player, bool isActive = false)
        {
            _players.Add(player);

            if (isActive)
            {
                SetActivePlayer(player);
            }
        }

        public Player GetNextPlayer(Player player)
        {
            var index = _players.IndexOf(player);

            if (index == _players.Count - 1)
            {
                return _players[0];
            } else
            {
                return _players[index + 1];
            }
        }

        public void SetActivePlayer(Player player)
        {
            if (player == _activePlayer)
            {
                return;
            }

            _players.ForEach((player) =>
            {
                player.SetActive(false);
            });

            player.SetActive(true);
            _activePlayer = player;

            HandleActivePlayerChange();
        }

        public void DestroyActivePlayer()
        {
            var activePlayer = GetActivePlayer();
            _players.Remove(activePlayer);
            DestroyPlayer(activePlayer);
        }

        public Player GetActivePlayer()
        {
            return _activePlayer;
        }

        public void DestroyAll()
        {
            foreach (var player in _players)
            {
                DestroyPlayer(player);
            }
        }

        private void DestroyPlayer(Player player)
        {
            Destroy(player.gameObject);
        }

        public PlayerStats GetStat(CharacterType type)
        {
            return _stats[type];
        }

        private void HandleActivePlayerChange()
        {
            OnActivePlayerChange?.Invoke(this, EventArgs.Empty);
        }
    }
}