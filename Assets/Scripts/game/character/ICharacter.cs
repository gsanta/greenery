using game.character.movement;
using game.character.state;
using UnityEngine;

namespace game.character
{
    public abstract class ICharacter : MonoBehaviour
    {
        private bool _isActive;

        public WeaponHolder WeaponHolder;

        public StateHandler States { get; private set; }

        public Movement Movement { get; protected set; }

        public void Construct(WeaponHolder weaponHolder)
        {
            WeaponHolder = weaponHolder;
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