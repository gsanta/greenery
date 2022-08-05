using game.character.ability.shoot.target;
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

        private IShootTarget _shootTarget;

        private bool _isAbilityActive = false;

        private float _speed = 15f;

        public float Speed { set { _speed = value; } }

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

        public void Construct(ICharacter character, BulletFactory bulletFactory, ICharacterStore<ICharacter> targetStore, IShootTarget shootTarget)
        {
            _bulletFactory = bulletFactory;
            _targetStore = targetStore;
            _character = character;
            _shootTarget = shootTarget;
        }
        
        public void Shoot()
        {
            var target = _shootTarget.GetTarget();
            var pos = transform.position;
            _bulletFactory.Create(_character, pos, target, _speed);
        }
    }
}
