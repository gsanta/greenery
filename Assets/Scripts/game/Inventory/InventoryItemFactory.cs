
using game.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace game.Item
{
    public class InventoryItemFactory : MonoBehaviour
    {

        [SerializeField] private Transform container;

        [SerializeField] private Image grass1Prefab;

        [SerializeField] private Image grass2Prefab;

        private InventoryStore _inventoryStore;

        private CursorHandler _cursorHandler;

        public void Construct(InventoryStore inventoryStore, CursorHandler cursorHandler)
        {
            _inventoryStore = inventoryStore;
            _cursorHandler = cursorHandler;
        }

        public InventoryItem CreateGrass1()
        {
            return CreateInventoryItem(ItemType.Grass1, grass1Prefab);
        }
        
        public InventoryItem CreateGrass2()
        {
            return CreateInventoryItem(ItemType.Grass2, grass2Prefab);
        }

        private InventoryItem CreateInventoryItem(ItemType type, Image prefab)
        {
            Image image = Instantiate(prefab, container);

            var item = new InventoryItem(type, image);

            GameObject gameObject = image.transform.gameObject;

            gameObject.AddComponent<EventTrigger>();

            var eventTrigger = gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry pointerOverEntry = new EventTrigger.Entry();
            pointerOverEntry.eventID = EventTriggerType.PointerEnter;
            pointerOverEntry.callback.AddListener((eventData) => _cursorHandler.ClearCursor());

            EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
            pointerClickEntry.eventID = EventTriggerType.PointerClick;
            pointerClickEntry.callback.AddListener((eventData) => _inventoryStore.SetActiveItem(item));
            eventTrigger.triggers.Add(pointerClickEntry);

            return item;
        }

    }
}
