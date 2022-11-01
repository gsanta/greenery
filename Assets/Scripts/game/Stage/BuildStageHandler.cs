
using Base.Input;
using game.item;
using game.Item;
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

        private InventoryHandler _inventoryHandler;

        public StageType Type { get; } = StageType.BuildStage;

        public BuildStageHandler(ItemHandler inputHandler, LevelStore levelStore, ScopedTileRenderer tileRenderer, InputHandler inputManager, InventoryHandler inventoryHandler)
        {
            _itemInputHandler = inputHandler;
            _levelStore = levelStore;
            _tileRenderer = tileRenderer;
            _inputHandler = inputManager;
            _inventoryHandler = inventoryHandler;
        }

        public void Activate()
        {
            _itemInputHandler.IsListenerDisabled = false;
            _tileRenderer.Show();
            _tileInputHandler = new TileHandler(_levelStore.ActiveLevel);
            _tileInputHandler.OnHoverTile += HandleTileHoverChange;
            _tileInputHandler.Register(_inputHandler);
            _inventoryHandler.SetActive(true);
        }

        public void Deactivate()
        {
            _itemInputHandler.IsListenerDisabled = true;
            _tileRenderer.Hide();
            _inventoryHandler.SetActive(false);
        }

        private void HandleTileHoverChange(object sender, OnHoverTileEventArgs args)
        {
            _tileRenderer.SetActiveNode(args.node);
        }
    }
}
