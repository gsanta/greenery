
using UnityEngine.UI;

namespace game.Inventory
{
    public class InventoryItem
    {
        public InventoryItemType type;

        public Image image;

        public InventoryItem(InventoryItemType type, Image image)
        {
            this.type = type;
            this.image = image;
        }
    }
}
