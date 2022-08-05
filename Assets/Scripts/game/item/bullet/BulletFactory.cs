using game.character;
using UnityEngine;

namespace game.item.bullet
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private game.item.bullet.Bullet bulletPrefab;
        [SerializeField] private Transform bulletContainer;

        public void Create(ICharacter parent, Vector3 pos, Vector3 dir, float speed)
        {
            var bullet = Instantiate(bulletPrefab, (Vector2) pos, Quaternion.identity, bulletContainer);
            bullet.Construct(parent, speed);
            bullet.SetDirection(dir);
        }
    }
}