using Base.Input;
using System.Collections.Generic;

namespace Game.Stage
{
    public class StageManager : InputListener
    {
        private StageHandler _activeStage;

        private List<StageHandler> _stageHandlers = new();

        public void Init()
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
            if (_activeStage != null)
            {
                _activeStage.Deactivate();
            }
            GetStageHandler(newStageType).Activate();
            
            _activeStage = GetStageHandler(newStageType);
        }

        public override void OnScroll(InputInfo inputInfo)
        {
            var nextStage = GetNextStage();
            ActivateStage(nextStage.Type);
        }

        private StageHandler GetNextStage()
        {
            var index = _stageHandlers.IndexOf(_activeStage);
            if (index == _stageHandlers.Count - 1)
            {
                return _stageHandlers[0];
            } else
            {
                return _stageHandlers[index + 1];
            }
        }

        private StageHandler GetStageHandler(StageType type)
        {
            return _stageHandlers.Find(handler => handler.Type == type);
        }
    }
}
