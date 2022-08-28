using game.item.bullet;
using game.tool;
using UnityEngine;

namespace Assets.Scripts.game.tool.weapon.bomb
{
    public class BombWeapon : IWeapon
    {
        private BulletFactory _bulletFactory;

        public BombWeapon(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public int Bullets { get; set; }

        public void OnFire(Vector2 pos)
        {
            var bullet = _bulletFactory.CreateBombBullet(pos);
            bullet.Fire();
        }
    }
}