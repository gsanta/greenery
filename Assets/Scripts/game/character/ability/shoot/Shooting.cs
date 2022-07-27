using game.item.bullet;
using UnityEngine;

namespace game.character.ability.shoot
{
    public class Shooting : MonoBehaviour, IAbility
    {
        public AbilityType AbilityType { get; } = AbilityType.Shoot;

        private ICharacter _character;
        
        private BulletFactory _bulletFactory;

        private ICharacterStore<ICharacter> _targetStore;

        private bool _isAbilityActive = false;
        
        public bool IsActive
        {
            get => _isAbilityActive;
            set
            {
                _isAbilityActive = value;

                if (value)
                {
                    InvokeRepeating(nameof(Shoot), 0, 2);
                }
                else
                {
                    CancelInvoke(nameof(Shoot));
                }
            }
        }

        public void Construct(ICharacter character, BulletFactory bulletFactory, ICharacterStore<ICharacter> targetStore)
        {
            _bulletFactory = bulletFactory;
            _targetStore = targetStore;
            _character = character;
        }
        
        public void Shoot()
        {
            var shootingDir = DirectionHelper.DirToVector(_character.GetMoveDirection());
            Debug.Log(_character.GetMoveDirection());
            var pos = transform.position;
            _bulletFactory.Create(_targetStore, pos, shootingDir);
        }
    }
}
