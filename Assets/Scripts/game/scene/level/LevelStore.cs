using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace game.scene.level
{
    public class LevelStore : MonoBehaviour
    {
        private List<Level> levels = new();

        public Level ActiveLevel { get; set; }

        public void AddLevel(Level level)
        {
            levels.Add(level);
        }

        public Level GetLevelByName(LevelName levelName)
        {
            return levels.Find((level) => level.levelName == levelName);
        }

        public string[] GetLevelNames()
        {
            return levels.Select((level) =>  Levels.LevelNameMap[level.levelName]).ToArray();
        }
    }
}