
using Base.Input;
using game.Item;
using UnityEngine;

namespace game.item
{
    public class ItemInputHandler : InputHandler
    {
        private InventoryStore _inventoryStore;

        private ItemFactory _itemFactory;

        public ItemInputHandler(InventoryStore inventoryStore, ItemFactory itemFactory) : base(InputHandlerType.ItemHandler)
        {
            _inventoryStore = inventoryStore;
            _itemFactory = itemFactory;
        }

        public override void OnClick(InputInfo inputInfo)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_inventoryStore.GetActiveItem() != null) {
                var activeItem = _inventoryStore.GetActiveItem();

                _itemFactory.Create(activeItem.type);
            }

        }
    }
}
