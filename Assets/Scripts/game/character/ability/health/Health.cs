using game.character.player;
using GUI;
using JetBrains.Annotations;
using UnityEngine;

namespace game.character.ability.health
{
    public class Health : MonoBehaviour
    {
        private int _maxHealth = 100;
        
        private int _currentHealth;

        private ICharacter _character;

        private PlayerStats _stats;

        [CanBeNull] private HealthBar _healthBar;
        
        public void Construct(ICharacter character, int maxHealth, [CanBeNull] HealthBar healthBar, PlayerStats stats)
        {
            _stats = stats;
            _character = character;
            _healthBar = healthBar;
            _maxHealth = stats.MaxLife;
            if (_healthBar != null) _healthBar.SetMaxHealth(maxHealth);
            SetHealth(_maxHealth);
        }
        
        public void ResetMaxHealth()
        {
            SetHealth(_maxHealth);
        }
        
        public void Decrease(int amount)
        {
            SetHealth(_currentHealth - amount);
        }

        private void SetHealth(int health)
        {
            health = health < 0 ? 0 : health;
            _currentHealth = health;
            if (_healthBar != null) _healthBar.SetHealth(health);

            _stats.Life = health;

            if (health == 0)
            {
                _character.Die();
            }
        }
    }
}