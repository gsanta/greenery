namespace Utils.math
{
    public struct IntPosition
    {
        public int X;
        public int Y;

        public IntPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public IntPosition((int x, int y) coords)
        {
            var (x, y) = coords;
            X = x;
            Y = y;
        }

        public static IntPosition Up = new IntPosition(0, 1);
        public static IntPosition Down = new IntPosition(0, -1);
        public static IntPosition Left = new IntPosition(-1, 0);
        public static IntPosition Right = new IntPosition(1, 0);
        public static IntPosition LeftDown = new IntPosition(-1, -1);
        public static IntPosition LeftUp = new IntPosition(-1, 1);
        public static IntPosition RightDown = new IntPosition(1, -1);
        public static IntPosition RightUp = new IntPosition(1, 1);
    }
}