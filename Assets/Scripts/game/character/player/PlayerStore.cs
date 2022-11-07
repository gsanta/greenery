using game.character.characters.enemy;
using game.character.player;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerStore : MonoBehaviour
    {
        public event EventHandler OnActivePlayerChange;

        private readonly List<ICharacter> _players = new();

        private Dictionary<CharacterType, PlayerStats> _stats = new Dictionary<CharacterType, PlayerStats>();

        private ICharacter _currentPlayer;

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

        public List<ICharacter> GetAll()
        {
            return _players;
        }

        public List<ICharacter> GetAll(PlayerType playerType)
        {
            return _players.FindAll(player => player.PlayerType == playerType).ToList();
        }

        public void Add(ICharacter player, bool isActive = false)
        {
            _players.Add(player);

            if (isActive)
            {
                SetCurrentPlayer(player);
            }
        }

        public void Remove(ICharacter character)
        {
            _players.Remove(character);
        }

        public int Count()
        {
            return _players.Count;
        }

        public ICharacter SetNextPlayer()
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

        public void SetCurrentPlayer(ICharacter player)
        {
            if (player == _currentPlayer)
            {
                return;
            }

            _currentPlayer = player;

            HandleCurrentPlayerChange();
        }

        public ICharacter GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public void DestroyAll()
        {
            foreach (var player in _players)
            {
                Destroy(player.gameObject);
            }
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