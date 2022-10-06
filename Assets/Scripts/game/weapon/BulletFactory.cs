using game.character;
using game.tool.weapon.bomb;
using UnityEngine;

namespace game.Item.bullet
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private GunBullet bulletPrefab;

        [SerializeField] private BombBullet bombPrefab;

        [SerializeField] private Transform bulletContainer;

        public void CreateGunBullet(ICharacter parent, Vector3 pos, Vector3 dir, float speed)
        {
            var bullet = Instantiate(bulletPrefab, (Vector2) pos, Quaternion.identity, bulletContainer);
            bullet.Construct(parent, speed);
            bullet.SetDirection(dir);
        }

        public BombBullet CreateBombBullet(Vector3 pos)
        {
            return Instantiate(bombPrefab, (Vector2)pos, Quaternion.identity, bulletContainer);
        }
    }
}