using gui;
using UnityEngine;

namespace game.tool
{
    public interface IWeapon
    {
        public int Bullets { get; set; }

        public void SetBulletPanel(BulletPanel bulletPanel);

        public void OnFire(Vector2 pos) { }

        public void AddBullet(int amount) { }
    }
}