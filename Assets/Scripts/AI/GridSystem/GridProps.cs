using UnityEngine;

namespace AI.GridSystem
{
    public class GridProps
    {
        public float CellSize { get; set; }
        public bool IsShowDebug { get; set; }
        public Vector2 LevelXExtent { get; set; }
        public Vector2 LevelYExtent { get; set; }
    }
}