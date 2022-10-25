
using game.weapon;
using System.Collections.Generic;
using UnityEngine;

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
            if (_activeItem != null)
            {
                _activeItem.Image.color = Color.white;
            }

            _activeItem = item;
            item.Image.color = Color.green;
        }

        public WeaponImage GetByType(WeaponType type)
        {
            return weaponImages.Find((image) => image.Type == type);
        }
    }
}
