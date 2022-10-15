using game.scene.level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage
{
    public class StageManager : MonoBehaviour
    {
        private StageType _currentStage;

        private List<StageHandler> _stageHandlers = new();

        private LevelLoader _levelLoader;

        public void Construct(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
            _levelLoader.LevelsStartedEventHandler += HandleLevelsStarted;
        }

        private void HandleLevelsStarted(object sender, EventArgs args)
        {
            DeactivateAllStages();
            ActivateStage(StageType.BuildStage);
        }

        public void AddStageHandler(StageHandler stageHandler)
        {
            _stageHandlers.Add(stageHandler);
        }

        public void DeactivateAllStages()
        {
            _stageHandlers.ForEach(handler => handler.Deactivate());
        }

        public void ActivateStage(StageType newStageType)
        {
            GetStageHandler(_currentStage).Deactivate();
            GetStageHandler(newStageType).Activate();
            
            _currentStage = newStageType;
        }

        private StageHandler GetStageHandler(StageType type)
        {
            return _stageHandlers.Find(handler => handler.Type == type);
        }
    }
}
