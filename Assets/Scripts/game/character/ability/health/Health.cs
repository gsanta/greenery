using game.character.characters.enemy;
using game.character.player;
using GUI;
using JetBrains.Annotations;
using UnityEngine;

namespace game.character.ability.health
{
    public class Health : MonoBehaviour
    {
        private int _maxHealth;
        
        private int _currentHealth;

        private ICharacter _character;

        private PlayerStats _stats;

        [CanBeNull] private HealthPanel _healthBar;
        
        public void Construct(ICharacter character, [CanBeNull] HealthPanel healthBar, PlayerStats stats)
        {
            _stats = stats;
            _character = character;
            _healthBar = healthBar;
            _maxHealth = stats.MaxLife;
            if (_healthBar != null) _healthBar.
                    SetMaxHealth(_maxHealth);
            SetHealth(stats.Life);
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