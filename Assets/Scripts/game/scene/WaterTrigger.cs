using game.character.ability.health;
using game.character.characters.player;
using UnityEngine;

namespace game.scene
{
    
    public class WaterTrigger : MonoBehaviour
    {
        private Health _health;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<Player>()) return;
            
            _health = other.gameObject.GetComponent<Health>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Health>() != _health) return;
        }
    }
}