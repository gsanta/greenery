
using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left,
    RightUp,
    RightDown,
    LeftUp,
    LeftDown,
}

public class DirectionHelper
{
    public static Vector2 DirToVectorDir(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.Right:
                return Vector2.right;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
            default:
                return Vector2.right;
        }
    }
}
