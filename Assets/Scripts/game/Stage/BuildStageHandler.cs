
using game.item;

namespace Game.Stage
{
    public class BuildStageHandler : StageHandler
    {
        private ItemInputHandler _inputHandler;

        public StageType Type { get; } = StageType.BuildStage;

        public BuildStageHandler(ItemInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        public void Activate()
        {
            _inputHandler.IsDisabled = false;
        }

        public void Deactivate()
        {
            _inputHandler.IsDisabled = true;
        }
    }
}
