
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace game.Inventory
{
    public class InventoryItemFactory : MonoBehaviour
    {

        [SerializeField] private Transform container;

        [SerializeField] private Image grass1Prefab;

        [SerializeField] private Image grass2Prefab;

        private InventoryStore _inventoryStore;

        public void Construct(InventoryStore inventoryStore)
        {
            _inventoryStore = inventoryStore;
        }

        public InventoryItem CreateGrass1()
        {
            return CreateInventoryItem(InventoryItemType.Grass1, grass1Prefab);
        }
        
        public InventoryItem CreateGrass2()
        {
            return CreateInventoryItem(InventoryItemType.Grass2, grass2Prefab);
        }

        private InventoryItem CreateInventoryItem(InventoryItemType type, Image prefab)
        {
            Image image = Instantiate(prefab, container);

            var item = new InventoryItem(type, image);

            GameObject gameObject = image.transform.gameObject;

            gameObject.AddComponent<EventTrigger>();

            var eventTrigger = gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { 
                _inventoryStore.SetActiveItem(item); 
            });
            eventTrigger.triggers.Add(entry);

            return item;
        }

    }
}
