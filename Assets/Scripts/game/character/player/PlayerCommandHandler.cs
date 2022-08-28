using game.character.player;
using gui;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerCommandHandler : MonoBehaviour
    {
        private PlayerStore _playerStore;

        private PlayerFactory _playerFactory;

        private BulletPanel _bulletPanel;

        private List<PlayerType> _playerTypes = new List<PlayerType> { PlayerType.Cat, PlayerType.Cow };

        public void Construct(PlayerStore playerStore, PlayerFactory playerFactory, BulletPanel bulletPanel)
        {
            _playerStore = playerStore;
            _playerFactory = playerFactory;
            _bulletPanel = bulletPanel;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var weapon = _playerStore.GetActivePlayer().Weapon;
                weapon.OnFire(pos);
                _bulletPanel.SetBullets(weapon.Bullets);
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                ChangePlayer();
            }
        }

        private void ChangePlayer()
        {
            var currentPlayer = _playerStore.GetActivePlayer();
            currentPlayer.Stats.Bullets = currentPlayer.Weapon.Bullets;
            var index = _playerTypes.IndexOf(currentPlayer.PlayerType);
            var newPlayerType = index == _playerTypes.Count - 1 ? _playerTypes[0] : _playerTypes[index + 1];

            _playerFactory.Create(currentPlayer.GetPosition(), newPlayerType);
            _playerStore.DestroyActivePlayer();
        }
    }
}