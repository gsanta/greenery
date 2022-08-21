using Assets.Scripts.game.tool.weapon.bomb;
using game.character;
using game.item.bullet;
using game.tool.weapon.bomb;
using game.tool.weapon.gun;
using UnityEngine;

namespace game.tool.weapon
{
    public class WeaponFactory : MonoBehaviour
    {

        private BulletFactory _bulletFactory;

        [SerializeField] private BombBullet bombPrefab;

        public void Construct(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public GunWeapon CreateGun(ICharacter character)
        {
            return new GunWeapon(character, _bulletFactory);
        }

        public BombWeapon CreateBomb(ICharacter character)
        {
            return new BombWeapon(_bulletFactory);

        }
    }
}