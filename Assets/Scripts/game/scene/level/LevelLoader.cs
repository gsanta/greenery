using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game.scene.level
{
    public class LevelLoadedEventArgs : EventArgs
    {
        public Scene Scene { get; set; }

        public LevelInjector LevelInjector { get; set; }
        
        public GameObject RootGameObject { get; set; }

        public Vector2 TranslateScene { get; set; }
    }

    public class LevelLoader : MonoBehaviour
    {

        public event EventHandler<LevelLoadedEventArgs> LevelLoadedEventHandler;

        public event EventHandler LevelsStartedEventHandler;

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
                eventArgs.LevelInjector = Array.Find(scene.GetRootGameObjects(), (obj) => obj.name == LevelInjector.UnityName).GetComponent<LevelInjector>();
                eventArgs.RootGameObject = Array.Find(scene.GetRootGameObjects(), (obj) => obj.name == "Root");

                eventArgs.LevelInjector.level.LevelStartedEventHandler += HandleLevelStarted;

                handler(this, eventArgs);
            }
        }

        private void HandleLevelStarted(object sender, LevelStartedEventArgs args)
        {
            _levelStore.GetLevelLoadingInfoByName(args.Level.levelName).IsStarted = true;

            if (_levelStore.GetLevelsToLoad().All(levelLoadingInfo => levelLoadingInfo.IsStarted))
            {
                EventHandler handler = LevelsStartedEventHandler;

                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
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