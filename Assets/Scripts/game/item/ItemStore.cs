using System.Collections.Generic;
using UnityEngine;

namespace game.item
{
    public class ItemStore<T> where T : MonoBehaviour
    {
        private readonly List<T> _items = new();

        public T GetClosest(Vector3 position, float maxRange)
        {
            T closest = null;

            foreach (var item in _items)        
            {
                if (Vector3.Distance(position, item.transform.position) <= maxRange)
                {
                    if (closest == null)
                    {
                        closest = item;
                    }
                    else
                    {
                        if (Vector3.Distance(position, item.transform.position) <=
                            Vector3.Distance(position, closest.transform.position))
                        {
                            closest = item;
                        }
                    }
                }
            }

            return closest;
        }

        public void RemoveItem(T item)
        {
            _items.Remove(item);
        }

        public void AddItem(T item)
        {
            this._items.Add(item);
        }
    }
}