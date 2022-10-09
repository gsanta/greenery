
using game.character.characters.enemy;
using game.character.player;

namespace Game.Stage
{
    public class FightStageHandler : StageHandler
    {
        private GunInputHandler _inputHandler;

        private EnemySpawner _enemySpawner;

        public StageType Type { get; } = StageType.FightStage;

        public FightStageHandler(GunInputHandler inputHandler, EnemySpawner enemySpawner)
        {
            _inputHandler = inputHandler;
            _enemySpawner = enemySpawner;
        }

        public void Activate()
        {
            _inputHandler.IsDisabled = false;
            _enemySpawner.IsDisabled = false;
        }

        public void Deactivate()
        {
            _inputHandler.IsDisabled = true;
            _enemySpawner.IsDisabled = true;
        }
    }
}
