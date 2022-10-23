
using game.weapon;
using System.Collections.Generic;

namespace Assetsgame.weapon
{
    public class WeaponImageStore
    {
        private List<WeaponImage> weaponImages = new();

        private WeaponImage _activeItem;

        public void Add(WeaponImage weaponImage)
        {
            weaponImages.Add(weaponImage);
        }

        public void SetActiveItem(WeaponImage item)
        {
            _activeItem = item;
        }

        public WeaponImage GetByType(WeaponType type)
        {
            return weaponImages.Find((image) => image.Type == type);
        }
    }
}
