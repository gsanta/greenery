using System;
using Items;
using UnityEngine;

namespace Players
{
    public class ItemPickup : MonoBehaviour
    {
        private ItemStore<Ball> _ballStore;
        
        public void Construct(ItemStore<Ball> ballStore)
        {
            _ballStore = ballStore;
        }
        
        private void Update()
        {   
            if (Input.GetMouseButtonDown(1))
            {
                var closest = _ballStore.GetClosest(transform.position, 1f);
                Destroy(closest.gameObject);
            }
        }
    }
}