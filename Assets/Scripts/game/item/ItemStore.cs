using System.Collections.Generic;

namespace game.Item
{
    public class ItemStore
    {

        List<ItemInfo> items = new();

        public void AddItem(ItemInfo itemInfo)
        {
            items.Add(itemInfo);
        }

    }
}
