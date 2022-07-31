using System;
using System.Collections.Generic;
using game.character.characters.player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game.scene.level
{
    public class LevelLoader : MonoBehaviour
    {
        private static LevelLoader _instance;

        private PlayerStore _playerStore;

        private List<Level> _levels = new();
        
        private HashSet<LevelName> _loadingLevels = new();
        
        public Level ActiveLevel { set; get; }

        public void Construct(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        public void AddLevel(Level level)
        {
            _levels.Add(level);
            _loadingLevels.Remove(level.levelName);

            if (!ActiveLevel)
            {
                ActiveLevel = level;
            }
        }
        
        public void LoadLevel(string levelName)
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        }

        private void Update()
        {
            if (ActiveLevel && _playerStore.GetActivePlayer())
            {
                var quarter = ActiveLevel.GetQuarter(_playerStore.GetActivePlayer().GetPosition());

                var levels = Levels.GetLevelsAtDirection(ActiveLevel, quarter);
                var oppositeLevels = Levels.GetLevelsAtOppositeDirection(ActiveLevel, quarter);

                levels.ForEach(ApplyNewLevel);
                oppositeLevels.ForEach(RemoveLevel);
            }
        }

        private void ApplyNewLevel(LevelName newLevel)
        {
            if (!_levels.Find((level) => level.levelName == newLevel) && !_loadingLevels.Contains(newLevel))
            {
                LoadLevel(Levels.LevelNameMap[newLevel]);
                _loadingLevels.Add(newLevel);
            }
        }

        private void RemoveLevel(LevelName levelName)
        {
            var level = _levels.Find((level) => level.levelName == levelName);
            if (level)
            {
                SceneManager.UnloadSceneAsync(Levels.LevelNameMap[levelName]);
                _levels.Remove(level);
            }
        }
    }
}