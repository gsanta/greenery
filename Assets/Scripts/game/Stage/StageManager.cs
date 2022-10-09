using Base.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage
{
    public class StageManager : MonoBehaviour
    {
        private StageType _currentStage;

        private List<StageHandler> _stageHandlers = new();

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
