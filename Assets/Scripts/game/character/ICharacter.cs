using game.character.ability;
using game.character.ability.health;
using game.character.movement;
using game.character.state;
using UnityEngine;

namespace game.character
{
    public abstract class ICharacter : MonoBehaviour
    {
        private bool _isActive;

        public StateHandler States { get; private set; }

        public void Construct()
        {
            States = new StateHandler();
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public virtual void Die() {}

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public IMovement Movement { get; protected set; }

        public GameObject GetGameObject() { return gameObject; }
    }
}