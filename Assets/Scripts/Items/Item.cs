using System;

namespace Items
{
    public enum ItemState
    {
        Spawned,
        PickedUp
    }
    
    public interface Item
    {
        public event EventHandler OnPickedUp;

        void SetItemState(ItemState state);
        ItemState GetItemState();
    }
}