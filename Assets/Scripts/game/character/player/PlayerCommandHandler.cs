using gui;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerCommandHandler : MonoBehaviour
    {
        private PlayerStore _playerStore;

        private PlayerFactory _playerFactory;

        private List<CharacterType> _playerTypes = new List<CharacterType> { CharacterType.Cat, CharacterType.Cow };

        public void Construct(PlayerStore playerStore, PlayerFactory playerFactory)
        {
            _playerStore = playerStore;
            _playerFactory = playerFactory;
        }

        private void ChangePlayer()
        {
            var currentPlayer = _playerStore.GetActivePlayer();
            var index = _playerTypes.IndexOf(currentPlayer.PlayerType);
            var newPlayerType = index == _playerTypes.Count - 1 ? _playerTypes[0] : _playerTypes[index + 1];

            _playerFactory.Create(currentPlayer.GetPosition(), newPlayerType);
        }
    }
}