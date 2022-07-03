using Characters.Players;
using Items.Bullet;
using UnityEngine;

namespace Characters
{
    public class Shooting : MonoBehaviour
    {
        private ICharacter _character;
        
        private BulletFactory _bulletFactory;

        private ICharacterStore<ICharacter> _targetStore;

        public void Construct(ICharacter character, BulletFactory bulletFactory, ICharacterStore<ICharacter> targetStore)
        {
            _bulletFactory = bulletFactory;
            _targetStore = targetStore;
            _character = character;
        }
        
        public void Shoot()
        {
            var shootingDir = DirectionHelper.DirToVectorDir(_character.GetMoveDirection());
            var pos = transform.position;
            _bulletFactory.Create(_targetStore, pos, shootingDir);
        }
    }
}
