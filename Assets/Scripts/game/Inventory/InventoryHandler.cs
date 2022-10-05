

using UnityEngine;

namespace game.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        private InventoryItemType _selectedInventoryItem;

        private InventoryItemFactory _inventoryItemFactory;

        private InventoryStore _inventoryStore;

        public void Construct(InventoryItemFactory inventoryItemFactory, InventoryStore inventoryStore)
        {
            _inventoryItemFactory = inventoryItemFactory;
            _inventoryStore = inventoryStore;
        }

        private void Start()
        {
            _inventoryStore.AddItem(_inventoryItemFactory.CreateGrass1());
            _inventoryStore.AddItem(_inventoryItemFactory.CreateGrass2());
            _inventoryStore.SetActiveItem(_inventoryStore.GetItemAt(0));
        }

        public void OnPanelEnter()
        {
            Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
            Debug.Log("Enter");
        }

        public void OnPanelExit()
        {
            Debug.Log("Exit");
        }
    }
}
