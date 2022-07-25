using Characters;
using UnityEngine;

namespace Items.Bullet
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletContainer;

        public void Create(ICharacterStore<ICharacter> targetStore, Vector3 pos, Vector3 dir)
        {
            var bullet = Instantiate(bulletPrefab, (Vector2) pos, Quaternion.identity, bulletContainer);
            bullet.Construct(targetStore);
            bullet.SetDirection(dir);
        }
    }
}