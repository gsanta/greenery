using game.character;
using UnityEngine;

namespace game.item.bullet
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private game.item.bullet.Bullet bulletPrefab;
        [SerializeField] private Transform bulletContainer;

        public void Create(Vector3 pos, Vector3 dir, float speed, ICharacterStore<ICharacter> targetStore)
        {
            var bullet = Instantiate(bulletPrefab, (Vector2) pos, Quaternion.identity, bulletContainer);
            bullet.Construct(speed, targetStore);
            bullet.SetDirection(dir);
        }
    }
}