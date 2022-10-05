
using System.Collections.Generic;
using UnityEngine;

namespace game.Inventory
{
    public class InventoryStore
    {
        private List<InventoryItem> _items = new();

        private InventoryItem _activeItem;

        public void AddItem(InventoryItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            _items.Remove(item);
        }

        public InventoryItem GetItemAt(int index)
        {
            return _items[index];
        }

        public void SetActiveItem(InventoryItem item)
        {
            if (_activeItem != null)
            {
                _activeItem.image.color = Color.white;
            }
            
            _activeItem = item;
            item.image.color = Color.green;
        }
    }
}
