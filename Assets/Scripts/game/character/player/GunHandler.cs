
using Base.Input;
using game.character.characters.player;
using gui;

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
            var player = _playerStore.GetCurrentPlayer();
            var direction = player.Movement.GetMoveDirection();
            var playerPos = player.GetPosition();

            var weapon = _playerStore.GetCurrentPlayer().WeaponHolder.GetActiveWeapon();
            weapon.OnFire(playerPos + DirectionHelper.DirToVector(direction));
            _bulletPanel.SetBullets(weapon.Bullets);
        }
    }
}
