
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


        public Image CreateGrass1()
        {
            return CreateInventoryItem(grass1Prefab);
        }
        
        public Image CreateGrass2()
        {
            return CreateInventoryItem(grass2Prefab);
        }

        private Image CreateInventoryItem(Image prefab)
        {
            Image image = Instantiate(prefab, container);
            GameObject gameObject = image.transform.gameObject;

            gameObject.AddComponent<EventTrigger>();

            var eventTrigger = gameObject.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { 
                Debug.Log("hello"); 
            });
            eventTrigger.triggers.Add(entry);

            return image;
        }

    }
}
