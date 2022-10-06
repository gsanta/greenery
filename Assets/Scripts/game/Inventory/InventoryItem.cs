
using UnityEngine.UI;

namespace game.Item
{
    public class InventoryItem
    {
        public ItemType type;

        public Image image;

        public InventoryItem(ItemType type, Image image)
        {
            this.type = type;
            this.image = image;
        }
    }
}
