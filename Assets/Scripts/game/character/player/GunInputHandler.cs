
using Base.Input;
using game.character.characters.player;
using gui;
using UnityEngine;

namespace game.character.player
{
    public class GunInputHandler : InputHandler
    {
        private PlayerStore _playerStore;

        private BulletPanel _bulletPanel;

        public GunInputHandler(PlayerStore playerStore, BulletPanel bulletPanel)
        : base(InputHandlerType.GunHandler
              ) {
            _playerStore = playerStore;
            _bulletPanel = bulletPanel;
        }

        public override void OnClick(InputInfo inputInfo)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var weapon = _playerStore.GetActivePlayer().Weapon;
            weapon.OnFire(pos);
            _bulletPanel.SetBullets(weapon.Bullets);
        }
    }
}
