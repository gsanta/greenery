
using game.tool;
using System.Collections.Generic;

namespace game.character
{
    public class WeaponHolder
    {

        private List<IWeapon> _weapons = new();

        private IWeapon _activeWeapon;

        public void AddWeapon(IWeapon weapon)
        {
            _weapons.Add(weapon);
        }

        public void ActivateWeapon(IWeapon weapon)
        {
            _activeWeapon = weapon;
        }

        public IWeapon GetWeaponAt(int index)
        {
            if (HasWeaponAt(index))
            {
                return _weapons[index];
            }

            return null;
        }

        private bool HasWeaponAt(int index)
        {
            return index >= 0 && _weapons.Count > index;
        }

        public IWeapon GetActiveWeapon()
        {
            return _activeWeapon;
        }

    }
}
