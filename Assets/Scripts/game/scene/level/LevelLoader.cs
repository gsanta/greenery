using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game.scene.level
{
    public class LevelLoader : MonoBehaviour
    {
        private Injector _injector;

        private GameManager _gameManager;

        private List<Level> _levels = new();
        
        private HashSet<LevelName> _loadingLevels = new();
        
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

        private void OnLevelLoaded(Scene scene)
        {
            var gameObject = Array.Find(scene.GetRootGameObjects(), (obj) =>
            {
                return obj.name == LevelInjector.UnityName;
            });

            gameObject.GetComponent<LevelInjector>().Construct(_injector);
        }

        private void LoadLevel(string levelName, Vector2 translate)
        {
            var operation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

            operation.completed += (s) =>
            {
                var rootObjects = SceneManager.GetSceneByName(levelName).GetRootGameObjects();
                var root = Array.Find(rootObjects, (gameObject) =>
                {
                    return gameObject.name == "Root";
                });

                root.transform.Translate(new Vector3(translate.x, translate.y, 0));

                OnLevelLoaded(SceneManager.GetSceneByName(levelName));
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