
namespace Base.Input
{
    public abstract class InputListener
    {
        public bool IsDisabled { get; set; }

        public virtual void OnKeyPressed(InputInfo inputInfo) { }

        public virtual void OnClick(InputInfo inputInfo) { }

        public virtual void OnMouseMove(InputInfo inputInfo) { }
        
        public virtual void OnScroll(InputInfo inputInfo) { }
    }
}
