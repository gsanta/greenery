using game.character.movement.path;
using game.character.player;
using game.character.state;
using game.Common;
using game.scene.grid;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace game.character
{
    public abstract class ICharacter : MonoBehaviour
    {
        [SerializeField] public Direction defaultHorizontalAnimationDirection = Direction.Left;

        public WeaponHolder WeaponHolder { get; private set; } = new WeaponHolder();

        public StateHandler States { get; private set; }

        public Movement Movement { get; set; }

        public PlayerType PlayerType { get; private set; }

        public GridGraph Grid { get; private set; }

        public MovementHandler MovementHandler;
        
        private List<GameObject> destroyables = new();

        private PathNode _pathNode;

        private CharacterEvents _characterEvents;

        public void Construct(PlayerType playerType, GridGraph grid, CharacterEvents characterEvents, MovementHandler movementHandler)
        {
            Grid = grid;
            PlayerType = playerType;
            States = new StateHandler();

            _characterEvents = characterEvents;
            MovementHandler = movementHandler;
        }

        public virtual void Die() {}

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public GameObject GetGameObject() { return gameObject; }

        public void AddDestroyable(GameObject gameObject)
        {
            destroyables.Add(gameObject);
        }

        public List<GameObject> GetDestroyables()
        {
            return destroyables;
        }

        public void RemoveDestoyable(GameObject gameObject)
        {
            destroyables.Remove(gameObject);
        }

        private void Update()
        {
            if (Movement.IsPaused)
            {
                return;
            }

            var pathNode = Grid.GetNodeAtWorldPos(GetPosition());

            if (_pathNode != pathNode)
            {
                var oldPathNode = _pathNode;
                _pathNode = pathNode;

                _characterEvents.EmitGridChange(this, oldPathNode, _pathNode);
            }

            Grid.GetNodeAtWorldPos(GetPosition()).character = this;
        }
    }
}