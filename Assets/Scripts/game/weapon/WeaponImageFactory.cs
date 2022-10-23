
using Assetsgame.weapon;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace game.weapon
{
    public class WeaponImageFactory : MonoBehaviour
    {
        [SerializeField] private Transform container;

        [SerializeField] private Image gunPrefab;

        [SerializeField] private Image bombPrefab;

        private WeaponImageStore _weaponImageStore;

        public void Construct(WeaponImageStore weaponImageStore)
        {
            _weaponImageStore = weaponImageStore;
        }

        public GameObject GetContainer()
        {
            return container.gameObject;
        }

        public void Create(WeaponType type)
        {
            var prefab = GetPrefab(type);
            Image image = Instantiate(prefab, container);

            _weaponImageStore.Add(new WeaponImage(type, image));

            GameObject gameObject = image.transform.gameObject;

            gameObject.AddComponent<EventTrigger>();
            var eventTrigger = gameObject.GetComponent<EventTrigger>();

            EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
            pointerClickEntry.eventID = EventTriggerType.PointerClick;
            pointerClickEntry.callback.AddListener((eventData) => _weaponImageStore.SetActiveItem(_weaponImageStore.GetByType(type)));
            eventTrigger.triggers.Add(pointerClickEntry);
        }

        private Image GetPrefab(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.Gun:
                    return gunPrefab;
                case WeaponType.Bomb:
                    return bombPrefab;
                default:
                    return null;
            }
        }
    }
}
