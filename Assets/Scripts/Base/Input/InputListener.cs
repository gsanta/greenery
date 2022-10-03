
namespace Base.Input
{
    public abstract class InputListener
    {
        public bool IsDisabled { get; set; }

        public virtual void OnKeyDown(InputInfo inputInfo) { }

        public virtual void OnClick(InputInfo inputInfo) { }
    }
}
