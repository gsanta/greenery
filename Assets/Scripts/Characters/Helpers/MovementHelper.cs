using System;
using UnityEngine;

namespace Characters.Helpers
{
    public static class MovementHelper
    {
        public static Direction UpdateMoveDirection(Vector3 movement, Direction currentDirection)
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

            return currentDirection;
        }
    }
}