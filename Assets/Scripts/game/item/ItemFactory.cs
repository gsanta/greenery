
using game.Item;
using UnityEngine;

namespace game.item
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;

        [SerializeField] private Sprite grass1Sprite;

        [SerializeField] private Sprite grass2Sprite;

        [SerializeField] private Transform container;

        public GameObject Create(ItemType itemType, Vector3 pos)
        {
            var gameObject = InstantiateGameObject(itemPrefab, pos);
            var sprite = GetSprite(itemType);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

            return gameObject;
        }

        private GameObject InstantiateGameObject(GameObject prefab, Vector3 pos)
        {
            return Instantiate(prefab, pos, Quaternion.identity, container);
        }


        private Sprite GetSprite(ItemType type)
        {
            switch(type)
            {
                case ItemType.Grass1:
                    return grass1Sprite;
                case ItemType.Grass2:
                    return grass2Sprite;
                default:
                    throw new System.Exception("Unknown type: " + type);
            }
        }
    }
}
