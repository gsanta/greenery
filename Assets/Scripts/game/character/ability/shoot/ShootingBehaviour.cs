using game.character.characters.enemy;
using game.character.characters.player;
using UnityEngine;

namespace game.character.ability.shoot
{
    public class ShootingBehaviour : MonoBehaviour
    {
        private Enemy _enemy;
        
        private PlayerStore _playerStore;

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

        public void Construct(Enemy enemy, PlayerStore playerStore)
        {
            _enemy = enemy;
            _playerStore = playerStore;
        }
        
        public void Shoot()
        {
            var target = _enemy.FieldOfView.FindTarget();
            if (target != null)
            {
                _enemy.WeaponHolder.GetActiveWeapon().OnFire(_playerStore.GetCurrentPlayer().GetPosition());
            }
        }
    }
}
