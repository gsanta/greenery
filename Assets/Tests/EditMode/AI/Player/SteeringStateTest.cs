using Characters;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.AI.Player
{
    internal class MoveAble : IMoveAble
    {
        private Vector2 _movement;
        public Vector2 Position;

        public MoveAble(Vector2 position)
        {
            Position = position;
        }

        public Vector2 GetPosition()
        {
            return Position;    
        }

        public void SetMovement(Vector2 movement)
        {
            _movement = movement;
        }

        public Vector2 GetMovement()
        {
            return _movement;
        }
    }
    
    
    public class SteeringStateTest
    {
        [Test]
        public void UpdateState_MovesTowardsTarget()
        {
            // var grid = new Grid<PathNode>(10, 10, (Grid<PathNode> grid, int x, int y) => new PathNode(grid, x, y), 1.0f);
            // var movable = new MoveAble(new Vector2(0, 0));

            // var steeringState = new SteeringState(grid, movable);
            //
            // steeringState.Activate();

            // movable.GetMovement();
        }
    }
}