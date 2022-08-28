using UnityEngine;

namespace game.tool
{
    public interface IWeapon
    {
        public int Bullets { get; set; }

        public void OnFire(Vector2 pos) { }
    }
}