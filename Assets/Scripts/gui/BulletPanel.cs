using UnityEngine;

namespace gui
{
    public class BulletPanel : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private Transform bulletContainer;

        public void SetBullets(int amount)
        {
            foreach (Transform child in bulletContainer)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < amount; i++)
            {
                Instantiate(bulletPrefab, bulletContainer);
            }

        }
    }
}