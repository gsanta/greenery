

using Base.Input;
using game.Common;
using UnityEngine;

namespace game.Item
{
    public class InventoryHandler : MonoBehaviour
    {
        private ItemType _selectedInventoryItem;

        private InventoryItemFactory _inventoryItemFactory;

        private InventoryStore _inventoryStore;

        private CursorHandler _cursorHandler;

        public void Construct(InventoryItemFactory inventoryItemFactory, InventoryStore inventoryStore, CursorHandler cursorHandler)
        {
            _inventoryItemFactory = inventoryItemFactory;
            _inventoryStore = inventoryStore;
            _cursorHandler = cursorHandler;
        }

        private void Start()
        {
            _inventoryStore.AddItem(_inventoryItemFactory.CreateGrass1());
            _inventoryStore.AddItem(_inventoryItemFactory.CreateGrass2());
            _inventoryStore.SetActiveItem(_inventoryStore.GetItemAt(0));
        }

        public void OnPanelEnter()
        {
            _cursorHandler.ClearCursor();
        }

        public void OnPanelExit()
        {
            if (!InputManager.IsPointerOverUIObject())
            {
                _cursorHandler.SetDefaultCursor();
            }
        }
    }
}
