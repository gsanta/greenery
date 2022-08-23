using game.character.player;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerCommandHandler : MonoBehaviour
    {
        private PlayerStore _playerStore;

        private PlayerFactory _playerFactory;

        private List<PlayerType> _playerTypes = new List<PlayerType> { PlayerType.Cat, PlayerType.Cow };

        public void Construct(PlayerStore playerStore, PlayerFactory playerFactory)
        {
            _playerStore = playerStore;
            _playerFactory = playerFactory;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _playerStore.GetActivePlayer().Weapon.OnFire(pos);
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                ChangePlayer();
            }
        }

        private void ChangePlayer()
        {
            var currentPlayer = _playerStore.GetActivePlayer();
            var index = _playerTypes.IndexOf(currentPlayer.PlayerType);
            var newPlayerType = index == _playerTypes.Count - 1 ? _playerTypes[0] : _playerTypes[index + 1];

            _playerFactory.Create(currentPlayer.GetPosition(), newPlayerType);
            _playerStore.DestroyActivePlayer();
        }
    }
}