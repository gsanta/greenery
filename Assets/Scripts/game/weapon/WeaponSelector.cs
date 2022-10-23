using Base.Input;
using game.character.characters.player;

namespace game.weapon
{
    public class WeaponSelector : InputListener
    {

        private PlayerStore _playerStore;

        public WeaponSelector(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        public override void OnKeyPressed(InputInfo inputInfo)
        {
            var weaponHolder = _playerStore.GetActivePlayer().WeaponHolder;
            weaponHolder.ActivateWeapontAt(inputInfo.GetNumberKeyPressed());
        }
    }
}
