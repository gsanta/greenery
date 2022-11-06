
using game.character.characters.enemy;
using game.character.player;
using game.weapon;

namespace Game.Stage
{
    public class FightStageHandler : StageHandler
    {
        private GunHandler _inputHandler;

        private EnemySpawner _enemySpawner;

        private WeaponHandler _weaponHandler;

        public StageType Type { get; } = StageType.FightStage;

        public FightStageHandler(GunHandler inputHandler, EnemySpawner enemySpawner, WeaponHandler weaponHandler)
        {
            _inputHandler = inputHandler;
            _enemySpawner = enemySpawner;
            _weaponHandler = weaponHandler;
        }

        public void Activate()
        {
            _inputHandler.IsListenerDisabled = false;
            //_enemySpawner.IsDisabled = false;
            //_weaponHandler.SetActive(true);
        }

        public void Deactivate()
        {
            _inputHandler.IsListenerDisabled = true;
            //_enemySpawner.IsDisabled = true;
            //_weaponHandler.SetActive(false);
        }
    }
}
