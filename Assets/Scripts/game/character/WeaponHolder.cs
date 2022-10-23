
using game.tool;
using System.Collections.Generic;

namespace game.character
{
    public class WeaponHolder
    {

        private List<IWeapon> weapons = new();

        private IWeapon _activeWeapon;

        public void AddWeapon(IWeapon weapon)
        {
            weapons.Add(weapon);
        }

        public void ActivateWeapontAt(int index)
        {
            if (HasWeaponAt(index))
            {
                _activeWeapon = weapons[index];
            }
        }

        private  bool HasWeaponAt(int index)
        {
            return index >= 0 && weapons.Count > index;
        }

        public IWeapon GetActiveWeapon()
        {
            return _activeWeapon;
        }

    }
}
