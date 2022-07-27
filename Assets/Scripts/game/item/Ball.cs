using System;
using UnityEngine;

namespace game.item
{
    public class Ball : MonoBehaviour, Item
    {

        private ItemState _itemState = ItemState.Spawned;
        
        public void SetItemState(ItemState state)
        {
            _itemState = state;
        }

        public ItemState GetItemState()
        {
            return _itemState;
        }

        public event EventHandler OnPickedUp;

        private void RaisePickedUp()
        {
            EventHandler handler = OnPickedUp;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}