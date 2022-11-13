
using UnityEngine;
using Utils.math;

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

public static class DirectionHelper
{
    private static readonly Vector2 LeftDown = new Vector2(-1, -1).normalized;
    private static readonly Vector2 LeftUp = new Vector2(-1, 1).normalized;
    private static readonly Vector2 RightDown = new Vector2(1, -1).normalized;
    private static readonly Vector2 RightUp = new Vector2(1, 1).normalized;
    public static Vector2 DirToVector(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Vector2.up,
            Direction.Right => Vector2.right,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.LeftDown => LeftDown,
            Direction.LeftUp => LeftUp,
            Direction.RightDown => RightDown,
            Direction.RightUp => RightUp,
            _ => Vector2.right
        };
    }
    
    public static Vector2Int DirToVectorInt(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Vector2Int.up,
            Direction.Right => Vector2Int.right,
            Direction.Down => Vector2Int.down,
            Direction.Left => Vector2Int.left,
            Direction.LeftDown => new Vector2Int(-1, -1),
            Direction.LeftUp => new Vector2Int(-1, 1),
            Direction.RightDown => new Vector2Int(1, -1),
            Direction.RightUp => new Vector2Int(1, 1),
            _ => Vector2Int.right
        };
    }
}
