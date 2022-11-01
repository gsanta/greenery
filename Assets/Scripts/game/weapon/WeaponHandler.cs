
using UnityEngine;

namespace game.weapon
{
    public class WeaponHandler : MonoBehaviour
    {
        private WeaponImageFactory _weaponImageFactory;

        private WeaponSelector _weaponSelector;

        public void Construct(WeaponImageFactory weaponImageFactory, WeaponSelector weaponSelector)
        {
            _weaponImageFactory = weaponImageFactory;
            _weaponSelector = weaponSelector;
        }

        private void Start()
        {
            _weaponImageFactory.Create(WeaponType.Gun);
            _weaponImageFactory.Create(WeaponType.Bomb);
            _weaponImageFactory.GetContainer().SetActive(false);
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                _weaponImageFactory.GetContainer().SetActive(true);
                _weaponSelector.IsListenerDisabled = false;
            } else
            {
                _weaponImageFactory.GetContainer().SetActive(false);
                _weaponSelector.IsListenerDisabled = true;
            }
        }
    }
}
