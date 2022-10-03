

using UnityEngine;
using UnityEngine.EventSystems;

namespace game.Inventory
{
    public class InventoryHandler : MonoBehaviour
    {
        private InventoryItemType _selectedInventoryItem;

        public void OnItemSelected(string item)
        {
            _selectedInventoryItem = InventoryItemMapper.GetType(item);
        }
    }
}
