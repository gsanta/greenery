using Assetsgame.weapon;
using Base.Input;
using game.character.characters.player;

namespace game.weapon
{
    public class WeaponSelector : InputListener
    {

        private PlayerStore _playerStore;

        private WeaponImageStore _weaponImageStore;

        public WeaponSelector(PlayerStore playerStore, WeaponImageStore weaponImageStore)
        {
            _playerStore = playerStore;
            _weaponImageStore = weaponImageStore;
        }

        public override void OnKeyPressed(InputInfo inputInfo)
        {
            if (inputInfo.Is1Pressed || inputInfo.Is2Pressed || inputInfo.Is3Pressed)
            {
                var weaponHolder = _playerStore.GetActivePlayer().WeaponHolder;
                var weapon = weaponHolder.GetWeaponAt(inputInfo.GetNumberKeyPressed());
                weaponHolder.ActivateWeapon(weapon);

                _weaponImageStore.SetActiveItem(_weaponImageStore.GetByType(weapon.Type));
            }

        }
    }
}
