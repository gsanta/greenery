using game.character;
using game.item.bullet;
using UnityEngine;

namespace game.tool.weapon.gun
{
    public class GunWeapon : ITool
    {
        private ICharacter _character;

        private BulletFactory _bulletFactory;

        private float _speed = 15f;

        public GunWeapon(ICharacter character, BulletFactory bulletFactory)
        {
            _character = character;
            _bulletFactory = bulletFactory;
        }

        public void OnFire(Vector2 target)
        {
            var position = _character.GetPosition();
            Vector2 direction = target - position;
            direction.Normalize();

            _bulletFactory.CreateGunBullet(_character, position, direction, _speed);
        }
    }
}