
using game.character.characters.player;
using game.item;
using game.scene.grid;
using game.scene.level;

namespace Game.Stage
{
    public class BuildStageHandler : StageHandler
    {
        private ItemInputHandler _inputHandler;

        private LevelStore _levelStore;

        private GridVisualizer _gridVisualizer;

        private PlayerStore _playerStore;

        public StageType Type { get; } = StageType.BuildStage;

        public BuildStageHandler(ItemInputHandler inputHandler, LevelStore levelStore, GridVisualizer gridVisualizer, PlayerStore playerStore)
        {
            _inputHandler = inputHandler;
            _levelStore = levelStore;
            _gridVisualizer = gridVisualizer;
            _playerStore = playerStore;
        }

        public void Activate()
        {
            _inputHandler.IsDisabled = false;
            var level = _levelStore.ActiveLevel;
            _gridVisualizer.SetGrid(level.Grid, level.RootGameObject.transform);
            _gridVisualizer.Show();

            _gridVisualizer.SetRadiusOrigin(_playerStore.GetActivePlayer().gameObject);
        }

        public void Deactivate()
        {
            _inputHandler.IsDisabled = true;
            _gridVisualizer.Hide();
        }
    }
}
