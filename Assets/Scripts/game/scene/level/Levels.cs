using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace game.scene.level
{
    public enum LevelName
    {
        Level,
        Level2,
        Level3,
        Level4,
        Level5
    }
    
    public static class Levels
    {
        public static readonly Dictionary<LevelName, string> LevelNameMap = new()
        {
            {LevelName.Level, "Level"},
            {LevelName.Level2, "Level2"},
            {LevelName.Level3, "Level3"},
            {LevelName.Level4, "Level4"},
            {LevelName.Level5, "Level5"},
        };

        public static readonly Dictionary<string, LevelName> ReverseNameMap = new();

        private static Dictionary<LevelName, Vector2Int> _levelPosMap = new()
        {
            {LevelName.Level, new Vector2Int(0, 0)},
            {LevelName.Level2, new Vector2Int(1, 0)},
            {LevelName.Level3, new Vector2Int(1, 1)},
            {LevelName.Level4, new Vector2Int(1, -1)},
            {LevelName.Level5, new Vector2Int(-1, 0)},
        };

        private static Dictionary<Vector2Int, LevelName> _reversePosMap = new();

        static Levels()
        {
            foreach (var (key, val) in _levelPosMap)
            {
                _reversePosMap[val] = key;
            }
            
            foreach (var (key, val) in LevelNameMap)
            {
                ReverseNameMap[val] = key;
            }
        }

        public static List<LevelName> GetLevelsAtDirection(Level currentLevel, Direction direction)
        {
            var currentPos = _levelPosMap[currentLevel.levelName];
            var ret = new List<LevelName>();
            
            switch (direction)
            {
                
                case Direction.RightUp:
                    AddRight(currentPos, ret);
                    AddTopRight(currentPos, ret);
                    break;
                case Direction.RightDown:
                    AddRight(currentPos, ret);
                    AddBottomRight(currentPos, ret);
                    break;
            }

            return ret;
        }

        public static List<LevelName> GetLevelsAtOppositeDirection(Level currentLevel, Direction direction)
        {
            var currentPos = _levelPosMap[currentLevel.levelName];
            var ret = new List<LevelName>();

            switch (direction)
            {
                
                case Direction.LeftUp:
                    AddLeft(currentPos, ret);
                    break;
            }

            return ret;
        }

        private static void AddRight(Vector2Int currentPos, ICollection<LevelName> list)
        {
            if (_reversePosMap.ContainsKey(currentPos + Vector2Int.right))
            {
                list.Add(_reversePosMap[currentPos + Vector2Int.right]);
            }
        }
        
        private static void AddLeft(Vector2Int currentPos, ICollection<LevelName> list)
        {
            if (_reversePosMap.ContainsKey(currentPos + Vector2Int.left))
            {
                list.Add(_reversePosMap[currentPos + Vector2Int.left]);
            }
        }
        
        private static void AddTopRight(Vector2Int currentPos, ICollection<LevelName> list)
        {
            if (_reversePosMap.ContainsKey(currentPos + Vector2Int.up + Vector2Int.right))
            {
                list.Add(_reversePosMap[currentPos + Vector2Int.up + Vector2Int.right]);
            }
        }
        
        private static void AddBottomRight(Vector2Int currentPos, ICollection<LevelName> list)
        {
            if (_reversePosMap.ContainsKey(currentPos + Vector2Int.down + Vector2Int.right))
            {
                list.Add(_reversePosMap[currentPos + Vector2Int.down + Vector2Int.right]);
            }
        }
    }
}