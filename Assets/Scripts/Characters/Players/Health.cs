using GUI;
using UnityEngine;

namespace Players
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth;

        private HealthBar _healthBar;

        public void Construct(HealthBar healthBar)
        {
            _healthBar = healthBar;
        }
        
        private void Start()
        {
            currentHealth = maxHealth;
            _healthBar.SetMaxHealth(maxHealth);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            _healthBar.SetHealth(currentHealth);
        }
    }
}