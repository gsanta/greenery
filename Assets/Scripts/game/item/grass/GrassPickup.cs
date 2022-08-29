using game.tool;
using UnityEngine;

namespace game.item.grass
{
    public class GrassPickup : MonoBehaviour, ItemPickup
    {
        private IWeapon _weapon;

        public void Construct(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public void Entered(GameObject gameObject)
        {
            if (Item.IsGrass(gameObject))
            {
                _weapon.AddBullet(1);
                Destroy(gameObject);
            }
        }
    }
}