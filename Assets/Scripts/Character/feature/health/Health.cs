using GUI;
using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Common
{
    public class Health : MonoBehaviour
    {
        private static readonly int _bulletDamage = 20;
        
        private int _maxHealth = 100;
        
        private int _currentHealth;

        private ICharacter _character;
        
        [CanBeNull] private HealthBar _healthBar;
        
        public void Construct(ICharacter character, int maxHealth, [CanBeNull] HealthBar healthBar)
        {
            _character = character;
            _healthBar = healthBar;
            _maxHealth = maxHealth;
            if (_healthBar != null) _healthBar.SetMaxHealth(maxHealth);
            SetHealth(_maxHealth);
        }
        
        public void ResetMaxHealth()
        {
            SetHealth(_maxHealth);
        }

        public void TakeDamage(int damage)
        {
            SetHealth(_currentHealth - damage);
        }
        
        public void HitByBullet()
        {
            SetHealth(_currentHealth - _bulletDamage);
        }

        private void SetHealth(int health)
        {
            health = health < 0 ? 0 : health;
            _currentHealth = health;
            if (_healthBar != null) _healthBar.SetHealth(health);

            if (health == 0)
            {
                _character.Die();
            }
        }
    }
}