using game.character;
using game.Item.bullet;
using game.weapon;
using gui;
using UnityEngine;

namespace game.tool.weapon.gun
{
    public class GunWeapon : IWeapon
    {
        private ICharacter _character;

        private BulletFactory _bulletFactory;

        private BulletPanel _bulletPanel;

        private float _speed = 15f;

        public int Bullets { get; set; } = -1;

        public WeaponType Type => WeaponType.Gun;

        public GunWeapon(ICharacter character, BulletFactory bulletFactory)
        {
            _character = character;
            _bulletFactory = bulletFactory;
        }

        public void OnFire(Vector2 target)
        {
            if (Bullets != 0)
            {
                var position = _character.GetPosition();
                Vector2 direction = target - position;
                direction.Normalize();

                _bulletFactory.CreateGunBullet(_character, position, direction, _speed);
                Bullets--;
            }
        }

        public void SetBulletPanel(BulletPanel bulletPanel)
        {
            _bulletPanel = bulletPanel;
        }
    }
}