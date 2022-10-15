using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace game.scene.level
{
    public class LevelStore : MonoBehaviour
    {
        private List<Level> levels = new();

        private List<LevelLoadingInfo> _levelLoadingInfoList = new();

        public Level ActiveLevel { get; set; }

        public void AddLevel(Level level)
        {
            levels.Add(level);

            if (!ActiveLevel)
            {
                ActiveLevel = level;
            }

            _levelLoadingInfoList.Find(levelLoadingInfo => levelLoadingInfo.levelName == level.levelName).IsLoaded = true;
        }

        public void AddLevelToLoad(LevelLoadingInfo levelLoadingInfo)
        {
            _levelLoadingInfoList.Add(levelLoadingInfo);
        }

        public List<LevelLoadingInfo> GetLevelsToLoad()
        {
            return _levelLoadingInfoList;
        }

        public bool IsLevelLoaded(LevelName levelName)
        {
            return _levelLoadingInfoList.Find(levelLoadingInfo => levelLoadingInfo.levelName == levelName).IsLoaded;
        }

        public int LevelCount()
        {
            return _levelLoadingInfoList.Count();
        }

        public LevelLoadingInfo GetLevelLoadingInfoByName(LevelName levelName)
        {
            return _levelLoadingInfoList.Find((level) => level.levelName == levelName);
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