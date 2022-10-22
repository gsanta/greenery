
using Base.Input;
using game.item;
using game.scene.grid;
using game.scene.level;

namespace Game.Stage
{
    public class BuildStageHandler : StageHandler
    {
        private ItemHandler _itemInputHandler;

        private readonly LevelStore _levelStore;
        
        private ScopedTileRenderer _tileRenderer;

        private readonly InputHandler _inputHandler;

        private TileHandler _tileInputHandler;

        public StageType Type { get; } = StageType.BuildStage;

        public BuildStageHandler(ItemHandler inputHandler, LevelStore levelStore, ScopedTileRenderer tileRenderer, InputHandler inputManager)
        {
            _itemInputHandler = inputHandler;
            _levelStore = levelStore;
            _tileRenderer = tileRenderer;
            _inputHandler = inputManager;
        }

        public void Activate()
        {
            _itemInputHandler.IsDisabled = false;
            _tileRenderer.Show();
            _tileInputHandler = new TileHandler(_levelStore.ActiveLevel);
            _tileInputHandler.OnHoverTile += HandleTileHoverChange;
            _inputHandler.AddHandler(_tileInputHandler);
        }

        public void Deactivate()
        {
            _itemInputHandler.IsDisabled = true;
            _tileRenderer.Hide();
        }

        private void HandleTileHoverChange(object sender, OnHoverTileEventArgs args)
        {
            _tileRenderer.SetActiveNode(args.node);
        }
    }
}
