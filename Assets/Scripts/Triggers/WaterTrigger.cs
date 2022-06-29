using Players;
using UnityEngine;

namespace Triggers
{
    
    public class WaterTrigger : MonoBehaviour
    {
        private Health _health;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<Player>()) return;
            
            _health = other.gameObject.GetComponent<Health>();
            InvokeRepeating(nameof(DecreaseHealth), 0, 1);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Health>() != _health) return;
            
            CancelInvoke(nameof(DecreaseHealth));
        }

        private void DecreaseHealth()
        {
            _health.TakeDamage(10);
        }
    }
}