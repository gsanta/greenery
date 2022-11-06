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

        private Player _currentPlayer;

        public PlayerStore()
        {
            var catStat = new PlayerStats(3);
            catStat.Bullets = 8;
            _stats.Add(CharacterType.Cat, catStat);

            var cowStat = new PlayerStats(5);
            cowStat.Bullets = 3;
            _stats.Add(CharacterType.Cow, catStat);

            _stats.Add(CharacterType.Player1, catStat);
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
                SetCurrentPlayer(player);
            }
        }

        public Player SetNextPlayer()
        {
            if (!_currentPlayer)
            {
                _currentPlayer = _players[0];
            } else
            {
                var index = _players.IndexOf(_currentPlayer);

                if (index == _players.Count - 1)
                {
                    _currentPlayer = _players[0];
                }
                else
                {
                    _currentPlayer = _players[index + 1];
                }
            }

            return _currentPlayer;
        }

        public void SetCurrentPlayer(Player player)
        {
            if (player == _currentPlayer)
            {
                return;
            }

            _players.ForEach((player) =>
            {
                player.IsCurrentPlayer = false;
            });

            player.IsCurrentPlayer = true;
            _currentPlayer = player;

            HandleCurrentPlayerChange();
        }

        public Player GetCurrentPlayer()
        {
            return _currentPlayer;
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

        private void HandleCurrentPlayerChange()
        {
            OnActivePlayerChange?.Invoke(this, EventArgs.Empty);
        }
    }
}