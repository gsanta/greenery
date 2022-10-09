
using game.item;
using game.scene.level;

namespace Game.Stage
{
    public class BuildStageHandler : StageHandler
    {
        private ItemInputHandler _inputHandler;

        private LevelStore _levelStore;

        public StageType Type { get; } = StageType.BuildStage;

        public BuildStageHandler(ItemInputHandler inputHandler, LevelStore levelStore)
        {
            _inputHandler = inputHandler;
            _levelStore = levelStore;
        }

        public void Activate()
        {
            _inputHandler.IsDisabled = false;
            _levelStore.ActiveLevel.gridVisualizer.Show();
        }

        public void Deactivate()
        {
            _inputHandler.IsDisabled = true;
            _levelStore.ActiveLevel.gridVisualizer.Hide();
        }
    }
}
