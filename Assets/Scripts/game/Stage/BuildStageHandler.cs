
using Base.Input;
using game.item;
using game.scene.grid;
using game.scene.level;

namespace Game.Stage
{
    public class BuildStageHandler : StageHandler
    {
        private ItemInputHandler _itemInputHandler;

        private readonly LevelStore _levelStore;
        
        private ScopedTileRenderer _tileRenderer;

        private readonly InputManager _inputManager;

        private TileInputHandler _tileInputHandler;

        public StageType Type { get; } = StageType.BuildStage;

        public BuildStageHandler(ItemInputHandler inputHandler, LevelStore levelStore, ScopedTileRenderer tileRenderer, InputManager inputManager)
        {
            _itemInputHandler = inputHandler;
            _levelStore = levelStore;
            _tileRenderer = tileRenderer;
            _inputManager = inputManager;
        }

        public void Activate()
        {
            _itemInputHandler.IsDisabled = false;
            _tileRenderer.Show();
            _tileInputHandler = new TileInputHandler(_levelStore.ActiveLevel);
            _tileInputHandler.OnHoverTile += HandleTileHoverChange;
            _inputManager.AddHandler(_tileInputHandler);
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
