
using Base.Input;
using game.character.characters.player;
using gui;
using UnityEngine;

namespace game.character.player
{
    public class GunHandler : InputListener
    {
        private PlayerStore _playerStore;

        private BulletPanel _bulletPanel;

        public GunHandler(PlayerStore playerStore, BulletPanel bulletPanel)
        {
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
