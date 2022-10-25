using game.weapon;
using gui;
using UnityEngine;

namespace game.tool
{
    public interface IWeapon
    {
        public WeaponType Type { get; }

        public int Bullets { get; set; }

        public void SetBulletPanel(BulletPanel bulletPanel);

        public void OnFire(Vector2 pos) { }

        public void AddBullet(int amount) { }
    }
}