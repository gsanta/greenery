
using Base.Input;
using game.Item;
using game.scene.level;
using UnityEngine;

namespace game.item
{
    public class ItemHandler : InputListener
    {
        private InventoryStore _inventoryStore;

        private ItemFactory _itemFactory;

        private LevelStore _levelStore;

        public ItemHandler(InventoryStore inventoryStore, ItemFactory itemFactory, LevelStore levelStore)
        {
            _inventoryStore = inventoryStore;
            _itemFactory = itemFactory;
            _levelStore = levelStore;
        }

        public override void OnClick(InputInfo inputInfo)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_inventoryStore.GetActiveItem() != null) {
                var activeItem = _inventoryStore.GetActiveItem();

                var gridPos = _levelStore.ActiveLevel.Grid.GetGridPosition(pos);
                if (gridPos.HasValue)
                {
                    var tileCenterPos = _levelStore.ActiveLevel.Grid.GetWorldPosition(gridPos.Value.x, gridPos.Value.y);
                    _itemFactory.Create(activeItem.type, new Vector3(tileCenterPos.x, tileCenterPos.y, 1));
                }

            }
        }
    }
}
