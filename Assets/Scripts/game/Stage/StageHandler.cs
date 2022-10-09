
namespace Game.Stage
{
    public interface StageHandler
    {
        public StageType Type { get; }

        public void Activate();
    
        public void Deactivate();
    }
}
