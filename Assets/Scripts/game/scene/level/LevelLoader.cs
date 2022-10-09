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

        public event EventHandler<LevelLoadedEventArgs> LevelLoadedEventHandler;

        private LevelStore _levelStore;

        public void Construct(LevelStore levelStore)
        {
            _levelStore = levelStore;
        }

        public void Load()
        {
            _levelStore.GetLevelsToLoad().ForEach(levelLoadingInfo => LoadLevel(Levels.LevelNameMap[levelLoadingInfo.levelName], levelLoadingInfo.position));
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
    }
}