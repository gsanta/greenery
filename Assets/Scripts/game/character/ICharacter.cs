using game.character.movement;
using game.character.movement.path;
using game.character.state;
using UnityEngine;

namespace game.character
{
    public abstract class ICharacter : MonoBehaviour
    {
        private bool _isActive;

        public WeaponHolder WeaponHolder { get; private set; } = new WeaponHolder();

        public StateHandler States { get; private set; }

        public LerpMover Movement { get; set; }

        public MovementPath MovementPath { get; set; }

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

        public GameObject GetGameObject() { return gameObject; }
    }
}