using UnityEngine;

namespace game.character.utils
{
    public static class MovementUtil
    {
        public static Direction UpdateMoveDirection(Vector2 movement, Direction? currentDirection)
        {
            if (movement.x != 0 && movement.y != 0)
            {
                switch (movement.x)
                {
                    case > 0 when movement.y > 0:
                        return Direction.RightUp;
                    case > 0 when movement.y < 0:
                        return Direction.RightDown;
                    case < 0 when movement.y > 0:
                        return Direction.LeftUp;
                    case < 0 when movement.y < 0:
                        return Direction.LeftDown;
                }
            } else switch (movement.x)
            {
                case 0 when movement.y > 0:
                    return Direction.Up;
                case 0 when movement.y < 0:
                    return Direction.Down;
                case < 0:
                    return Direction.Left;
                case > 0:
                    return Direction.Right;
            }

            return currentDirection != null ? (Direction) currentDirection : Direction.Left;
        }
    }
}