using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game.scene.level
{
    public class LevelLoadedEventArgs : EventArgs
    {
        public Scene Scene { get; set; }

        public Vector2 TranslateScene { get; set; }
    }

    public class LevelLoader : MonoBehaviour
    {
        private Injector _injector;

        private GameManager _gameManager;

        private List<Level> _levels = new();
        
        private HashSet<LevelName> _loadingLevels = new();

        public event EventHandler<LevelLoadedEventArgs> LevelLoadedEventHandler;

        public Level ActiveLevel { set; get; }

        public void Construct(Injector injector)
        {
            _injector = injector;
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
        
        public void Load(string levelName)
        {
            LoadLevel("Level", new Vector2(0, 0));
            LoadLevel("Level2", new Vector2(28, 0));
        }

        private void OnLevelLoaded(Scene scene, Vector2 translate)
        {

            EventHandler<LevelLoadedEventArgs> handler = LevelLoadedEventHandler;
            if (handler != null)
            {
                var eventArgs = new LevelLoadedEventArgs();
                eventArgs.Scene = scene;
                eventArgs.TranslateScene = translate;
                handler(this, eventArgs);
            }
        }

        private void LoadLevel(string levelName, Vector2 translate)
        {
            var operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

            operation.completed += (s) =>
            {
                OnLevelLoaded(SceneManager.GetSceneByName(levelName), translate);
            };
        }

        private void Update()
        {
            //if (ActiveLevel && _playerStore.GetActivePlayer())
            //{
            //    var quarter = ActiveLevel.GetQuarter(_playerStore.GetActivePlayer().GetPosition());

            //    var levels = Levels.GetLevelsAtDirection(ActiveLevel, quarter);
            //    var oppositeLevels = Levels.GetLevelsAtOppositeDirection(ActiveLevel, quarter);

            //    levels.ForEach(ApplyNewLevel);
            //    oppositeLevels.ForEach(RemoveLevel);
            //}
        }

        private void ApplyNewLevel(LevelName newLevel)
        {
            //if (!_levels.Find((level) => level.levelName == newLevel) && !_loadingLevels.Contains(newLevel))
            //{
            //    LoadLevel(Levels.LevelNameMap[newLevel]);
            //    _loadingLevels.Add(newLevel);
            //}
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