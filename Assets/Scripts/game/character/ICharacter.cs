using game.character.movement;
using game.character.movement.path;
using game.character.state;
using game.Common;
using UnityEngine;

namespace game.character
{
    public abstract class ICharacter : MonoBehaviour
    {
        [SerializeField] public Direction defaultHorizontalAnimationDirection = Direction.Left;

        public WeaponHolder WeaponHolder { get; private set; } = new WeaponHolder();

        public StateHandler States { get; private set; }

        public Movement Movement { get; set; }

        public Activateable PathFinder { get; set; }

        public void Construct()
        {
            States = new StateHandler();
        }

        public virtual void Die() {}

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public GameObject GetGameObject() { return gameObject; }
    }
}