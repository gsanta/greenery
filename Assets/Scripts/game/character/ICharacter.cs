using game.character.characters.enemy;
using game.character.characters.player;
using game.character.movement.path;
using game.character.state;
using game.scene.grid;
using System.Collections.Generic;
using UnityEngine;

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

        private List<GameObject> destroyables = new();

        public void Construct(PlayerType playerType, GridGraph grid)
        {
            Grid = grid;
            PlayerType = playerType;
            States = new StateHandler();
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

            Grid.GetNodeAtWorldPos(GetPosition()).character = this;
        }
    }
}