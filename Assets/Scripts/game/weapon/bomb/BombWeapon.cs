using game.item.bullet;
using game.tool;
using gui;
using UnityEngine;

namespace Assets.Scripts.game.tool.weapon.bomb
{
    public class BombWeapon : IWeapon
    {
        private BulletFactory _bulletFactory;

        private BulletPanel _bulletPanel;

        public BombWeapon(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public int Bullets { get; set; }

        public void AddBullet(int amount) {
            Bullets += amount;

            if (_bulletPanel)
            {
                _bulletPanel.SetBullets(Bullets);
            }
        }

        public void OnFire(Vector2 pos)
        {
            if (Bullets > 0)
            {
                var bullet = _bulletFactory.CreateBombBullet(pos);
                bullet.Fire();

                Bullets--;
            }
        }

        public void SetBulletPanel(BulletPanel bulletPanel)
        {
            _bulletPanel = bulletPanel;
        }
    }
}