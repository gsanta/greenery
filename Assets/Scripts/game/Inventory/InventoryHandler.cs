

using UnityEngine;

namespace game.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        private InventoryItemType _selectedInventoryItem;

        private InventoryItemFactory _inventoryItemFactory;

        public void Construct(InventoryItemFactory inventoryItemFactory)
        {
            _inventoryItemFactory = inventoryItemFactory;
        }

        private void Start()
        {
            _inventoryItemFactory.CreateGrass1();       
            _inventoryItemFactory.CreateGrass2();       
        }
    }
}
